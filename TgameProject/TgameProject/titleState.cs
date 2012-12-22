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
using GameInterfaces;

namespace Tgame
{
    /// <summary>
    /// DrawableGameComponent インターフェイスを実装したゲーム コンポーネントです。
    /// タイトル画面のステップ全体を統括するクラス
    /// </summary>
    public class titleState : Tgame.TgameStage
    {
        private SpriteBatch spriteBatch;
        private SpriteFont menuFont,onFont;
        private Texture2D TmousePointer;
        public MouseState mouseState;
        private ClickAbleText Play,Continue,Option,Exit;
        public titleState(Game1 game)
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
            // TODO: ここに初期化のコードを追加します。
            Vector2 centerPos = new Vector2((GraphicsDevice.Viewport.TitleSafeArea.Left+GraphicsDevice.Viewport.TitleSafeArea.Right)/2,
                (GraphicsDevice.Viewport.TitleSafeArea.Top+GraphicsDevice.Viewport.TitleSafeArea.Bottom)/2);
            
            Play.centerPos = new Vector2(centerPos.X,centerPos.Y-80);
            Continue.centerPos = new Vector2(centerPos.X, centerPos.Y - 40);
            Option.centerPos = new Vector2(centerPos.X, centerPos.Y );
            Exit.centerPos = new Vector2(centerPos.X, centerPos.Y + 40);
            this.Play.txtMouseOnEvent += new MouseOn(DoNone);
            this.Play.txtClickedEvent += new Clicked(StartPlay);
            this.Continue.txtMouseOnEvent += new MouseOn(DoNone);
            this.Continue.txtClickedEvent += new Clicked(DoNone);
            this.Option.txtMouseOnEvent += new MouseOn(DoNone);
            this.Option.txtClickedEvent += new Clicked(DoNone);
            this.Exit.txtMouseOnEvent += new MouseOn(DoNone);
            this.Exit.txtClickedEvent += new Clicked(GameExit);
            base.Initialize();
        }
        /// <summary>
        /// 必要なコンテンツの読み込みを行います
        /// </summary>
        protected override void LoadContent()
        {
            Content.RootDirectory = @"Content\titileContent";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TmousePointer = Content.Load<Texture2D>("mouseTest");
            menuFont = Content.Load<SpriteFont>("DefFont");
            onFont = Content.Load<SpriteFont>("OnFont");
            this.Play = new ClickAbleText("Play", menuFont,onFont);
            this.Continue = new ClickAbleText("Continue", menuFont, onFont);
            this.Option = new ClickAbleText("Option", menuFont, onFont);
            this.Exit = new ClickAbleText("Exit", menuFont, onFont);
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
            Play.txtUpdate(mouseState);
            Continue.txtUpdate(mouseState);
            Option.txtUpdate(mouseState);
            Exit.txtUpdate(mouseState);
            base.Update(gameTime);
        }
        /// <summary>1
        /// ゲーム コンポーネントが自身を描画するためのメソッドです。
        /// </summary>
        /// <param name="gameTime">ゲームの瞬間的なタイミング情報</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            this.spriteBatch.Draw(TmousePointer, new Rectangle(mouseState.X, mouseState.Y, TmousePointer.Width, TmousePointer.Height), Color.Beige);
            this.spriteBatch.DrawString(Play.GetStateFont(), Play.txt, Play.pos, Color.Black);
            this.spriteBatch.DrawString(Continue.GetStateFont(), Continue.txt, Continue.pos, Color.Black);
            this.spriteBatch.DrawString(Option.GetStateFont(), Option.txt, Option.pos, Color.Black);
            this.spriteBatch.DrawString(Exit.GetStateFont(), Exit.txt, Exit.pos, Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DoNone(){
            return; 
        }
        private void GameExit() {
            StageEnd(GameStates.Exit);
            return;
        }
        private void StartPlay() {
            StageEnd(GameStates.Play);
            return;
        }
    }
}
