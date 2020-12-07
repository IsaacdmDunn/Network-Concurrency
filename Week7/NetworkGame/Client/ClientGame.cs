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

namespace GameClient
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ClientGame : Game
    {
        //ClientForm mClientForm;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D player;
        float targetX = 128;
        float targetY;
        Vector2 scale;
        Vector2 velocity = new Vector2(100, 100);
        Vector2 position = new Vector2(0, 0);
        
        TcpClient tcpClient;
        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;
        BinaryFormatter formatter;
        bool isConnected = false;

        public ClientGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            tcpClient = new TcpClient();
        }

        //connects to server
        public bool Connect(string ipAddreess, int port)
        {
            try
            {
                //connects to server
                tcpClient.Connect(ipAddreess, port);
                stream = tcpClient.GetStream();
                writer = new BinaryWriter(stream, Encoding.UTF8);
                reader = new BinaryReader(stream, Encoding.UTF8);
                formatter = new BinaryFormatter();

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
            Thread threads = new Thread(ProcessServerResponse);
            threads.Start();
            //mClientForm.ShowDialog();

        }

        //processes server responce
        private void ProcessServerResponse()
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

                    }
                }
            }

            reader.Close();
            writer.Close();
            tcpClient.Close();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            player = Content.Load<Texture2D>("Player");
            scale = new Vector2(targetX / (float)player.Width, targetX / (float)player.Width);
            targetY = player.Height * scale.Y;

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.D))
                position.X += 10;
            if (state.IsKeyDown(Keys.A))
                position.X -= 10;
            if (state.IsKeyDown(Keys.W))
                position.Y -= 10;
            if (state.IsKeyDown(Keys.S))
                position.Y += 10;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BlueViolet);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(player, position: position, scale: scale);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
