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
    public delegate void StatesEnd(GameStates nowState);
    public enum GameStates {
        Exit,
        Title,
        Play,
    
    };
    /// <summary>
    /// 基底 Game クラスから派生した、ゲームのメイン クラスです。
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private TgameStage nowStage;
        public KeyboardState oldKeyState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// ゲームが実行を開始する前に必要な初期化を行います。
        /// ここで、必要なサービスを照会して、関連するグラフィック以外のコンテンツを
        /// 読み込むことができます。base.Initialize を呼び出すと、使用するすべての
        /// コンポーネントが列挙されるとともに、初期化されます。
        /// </summary>
        protected override void Initialize()
        {
            // TODO: ここに初期化ロジックを追加します。

            //this.graphics.ToggleFullScreen();
            nowStage = new titleState(this);
            this.Components.Add(nowStage);
            nowStage.stageEnd+=new StatesEnd(setNextState);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent はゲームごとに 1 回呼び出され、ここですべてのコンテンツを
        /// 読み込みます。
        /// </summary>
        protected override void LoadContent()
        {
            // 新規の SpriteBatch を作成します。これはテクスチャーの描画に使用できます。
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: this.Content クラスを使用して、ゲームのコンテンツを読み込みます。
        }

        /// <summary>
        /// UnloadContent はゲームごとに 1 回呼び出され、ここですべてのコンテンツを
        /// アンロードします。
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: ここで ContentManager 以外のすべてのコンテンツをアンロードします。
        }

        /// <summary>
        /// ワールドの更新、衝突判定、入力値の取得、オーディオの再生などの
        /// ゲーム ロジックを、実行します。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲームの終了条件をチェックします。
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            // TODO: ここにゲームのアップデート ロジックを追加します。
            

            base.Update(gameTime);
            oldKeyState = Keyboard.GetState();
        }

        /// <summary>
        /// ゲームが自身を描画するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: ここに描画コードを追加します。
            spriteBatch.Begin();
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void setNextState(GameStates nextState)
        {
            this.Components.Remove(nowStage);
            switch (nextState) {
                case GameStates.Exit:
                    this.Exit();
                    break;
                case GameStates.Title:
                    nowStage = new titleState(this);
                    break;
                case GameStates.Play:
                    nowStage = new TgamePlay(this);
                    break;
            }
            this.Components.Add(nowStage);
            return;
        }
    }
    /// <summary>
    /// DrawableGameComponent インターフェイスを実装したゲーム コンポーネントです。
    /// 継承されることが前提であり、本ゲームの各ステート(大きな流れのこと）に用いられることを想定しています。
    /// </summary>
    public class TgameStage : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public event StatesEnd stageEnd;
        protected ContentManager Content;
        public TgameStage(Game1 game)
            : base(game)
        {
            Content = new ContentManager(game.Services);
            // TODO: ここで子コンポーネントを作成します。
            base.Initialize();
        }

        /// <summary>
        /// ゲーム コンポーネントの初期化を行います。
        /// ここで、必要なサービスを照会して、使用するコンテンツを読み込むことができます。
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        /// 必要なコンテンツの読み込みを行います
        /// </summary>
        protected override void LoadContent()
        {
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
            base.Update(gameTime);
        }
        /// <summary>
        /// ゲーム コンポーネントが自身を描画するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        protected void StageEnd(GameStates i)
        {
            stageEnd(i);
            return;
        }
    }
}
