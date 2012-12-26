using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Tgame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TgamePlay : Tgame.TgameStage
    {
        private SpriteBatch spriteBatch;
        public MouseState mouseState;
        public KeyboardState keyState;
        protected Enemies enemies;
        protected Player player;
        protected PlayerCamera mainCamera;
        public TgamePlay(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here
            base.Initialize();
        }
        
        /// <summary>
        /// ゲーム コンポーネントの初期化を行います。
        /// ここで、必要なサービスを照会して、使用するコンテンツを読み込むことができます。
        /// </summary>
        public override void Initialize()
        {
            // TODO: ここに初期化のコードを追加します。
            base.Initialize();
        }
        /// <summary>
        /// 必要なコンテンツの読み込みを行います
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Content.RootDirectory = @"Content\gameContent";
            Model model1 = this.Content.Load<Model> ("untitled");
            CharactorPalameter playerPara = new CharactorPalameter();
            playerPara.pos = new Vector3(0);
            playerPara.Dir = new Vector3(0, 0, 1);
            playerPara.MaxSpeed = 1;
            player = new Player(this.Content.Load<Model>("untitled"), playerPara);
            enemies = new Enemies();
            CharactorPalameter planePara = new CharactorPalameter();
            planePara.pos = new Vector3(0,0,10);
            planePara.Dir = new Vector3(1,0,0);
            PlaneEnemy plane = new PlaneEnemy(this.Content.Load<Model>("untitled"), planePara);
            enemies.Add(plane);


            mainCamera = new PlayerCamera(player, MathHelper.ToRadians(45.0f),
                                          (float)this.GraphicsDevice.Viewport.Width /
                                            (float)this.GraphicsDevice.Viewport.Height);

            base.LoadContent();
        }
        /// <summary>
        /// 必要なコンテンツの読み込みを行います
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// ゲーム コンポーネントが自身を更新するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: ここにアップデートのコードを追加します。
            this.mouseState = Mouse.GetState();
            this.keyState = Keyboard.GetState();
            player.Input(keyState);
            enemies.Input();
            mainCamera.Input(mouseState);


            player.UpDate();
            enemies.UpDate();
            mainCamera.UpDate();
            base.Update(gameTime);
        }
        /// <summary>1
        /// ゲーム コンポーネントが自身を描画するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        public override void Draw(GameTime gameTime)
        {
            player.Draw(mainCamera);
            enemies.Draw(mainCamera);
            base.Draw(gameTime);
        }
    }
}
