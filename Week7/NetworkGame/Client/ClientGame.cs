using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Packets;
using System.Net;

namespace GameClient
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ClientGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player[] player;
        public Texture2D playerTex;
        public Vector2 scale;

        public float targetX = 128;
        public float targetY;

        UdpClient udpClient;
        TcpClient tcpClient;
        IPEndPoint mEndpoint = new IPEndPoint(IPAddress.Any, 0);
        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;
        BinaryFormatter formatter;
        bool isConnected = false;

        public ClientGame()
        {

            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set up tcp client and connect to server
            udpClient = new UdpClient();
            tcpClient = new TcpClient();
            Connect("127.0.0.1", 4444);
            RunClient();

            //initialise players
            player = new Player[5];
            for (int i = 0; i < 5; i++)
            {
                player[i] = new Player();
            }

        }

        //connects to server
        public bool Connect(string ipAddreess, int port)
        {
            try
            {
                //connects to server
                udpClient.Connect(ipAddreess, port);
                tcpClient.Connect(ipAddreess, port);
                stream = tcpClient.GetStream();
                writer = new BinaryWriter(stream, Encoding.UTF8);
                reader = new BinaryReader(stream, Encoding.UTF8);
                formatter = new BinaryFormatter();
                isConnected = true;
                return true;
            }
            //if client fails to connect
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public void RunClient()
        {
            //mClientForm = new ClientForm(this);

            //sets up new thread
            Thread UDPThread = new Thread(UDPProcessServerResponse);
            Thread TCPThread = new Thread(TCPProcessServerResponse);

            Login();

            UDPThread.Start();
            TCPThread.Start();

        }

        //processes server responce
        private void UDPProcessServerResponse()
        {
           
            //if connected to server
            
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
                while (true)
                {
                    byte[] buffer = udpClient.Receive(ref endPoint);
                    MemoryStream _stream = new MemoryStream(buffer);
                    Packet recievedPackage = formatter.Deserialize(_stream) as Packet;

                    //if packet type is...
                    switch (recievedPackage.mPacketType)
                    {
                        //position data is set to player object
                        case PacketType.positionData:
                            PositionPacket positionDataPacket = (PositionPacket)recievedPackage;
                            player[1].position.X = positionDataPacket.mPosX;
                            player[1].position.Y = positionDataPacket.mPosY;
                            break;
                        
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Client UDP Read Method exception: " +  e.Message);
            }
            

            reader.Close();
            writer.Close();
            udpClient.Close();
        }

        //processes server responce
        private void TCPProcessServerResponse()
        {
            int numberOfBytes;
            //if connected to server
            while (isConnected == true)
            {
                //read data from server and deserialize
                if ((numberOfBytes = reader.ReadInt32()) != 0)
                {
                    byte[] buffer = reader.ReadBytes(numberOfBytes);
                    MemoryStream _stream = new MemoryStream(buffer);
                    Packet recievedPackage = formatter.Deserialize(_stream) as Packet;

                    //if packet type is...
                    switch (recievedPackage.mPacketType)
                    {
                        //send message to all
                        case PacketType.login:
                            LoginPacket loginPacket = (LoginPacket)recievedPackage;
                            break;

                    }
                }
            }

            reader.Close();
            writer.Close();
            tcpClient.Close();
        }

        //serialize position data and send to server
        public void UDPSendPosition()
        {
            PositionPacket positionDataPacket = new PositionPacket(player[0].position.X, player[0].position.Y);
            MemoryStream msgStream = new MemoryStream();
            formatter.Serialize(msgStream, positionDataPacket);
            byte[] buffer = msgStream.GetBuffer();
            udpClient.Send(buffer, buffer.Length);
        }

        public void Login()
        {
            LoginPacket loginPacket = new LoginPacket((IPEndPoint)udpClient.Client.LocalEndPoint);
            MemoryStream msgStream = new MemoryStream();
            formatter.Serialize(msgStream, loginPacket);
            byte[] buffer = msgStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load player sprite and set up scale and target
            playerTex = Content.Load<Texture2D>("Player");
            scale = new Vector2(targetX / (float)playerTex.Width, targetX / (float)playerTex.Width);
            targetY = playerTex.Height * scale.Y;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //exit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //move player
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D))
                player[0].position.X += 10;
            if (state.IsKeyDown(Keys.A))
                player[0].position.X -= 10;
            if (state.IsKeyDown(Keys.W))
                player[0].position.Y -= 10;
            if (state.IsKeyDown(Keys.S))
                player[0].position.Y += 10;

            //serialize position as packet and send to server
            UDPSendPosition();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Maroon);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //draw players
            for (int i = 0; i < 5; i++)
            {
                spriteBatch.Draw(playerTex, position: player[i].position, scale: scale);
                
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    //player class
    public class Player
    {
        public Vector2 velocity;
        public Vector2 position;

        public Player()
        {
            velocity = new Vector2(100, 100);
            position = new Vector2(0, 0);
        }

    }
}
