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
        /// �Q�[�� �R���|�[�l���g�̏��������s���܂��B
        /// �����ŁA�K�v�ȃT�[�r�X���Ɖ�āA�g�p����R���e���c��ǂݍ��ނ��Ƃ��ł��܂��B
        /// </summary>
        public override void Initialize()
        {
            // TODO: �����ɏ������̃R�[�h��ǉ����܂��B
            base.Initialize();
        }
        /// <summary>
        /// �K�v�ȃR���e���c�̓ǂݍ��݂��s���܂�
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
        /// �K�v�ȃR���e���c�̓ǂݍ��݂��s���܂�
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }
        /// <summary>
        /// �Q�[�� �R���|�[�l���g�����g���X�V���邽�߂̃��\�b�h�ł��B
        /// </summary>
        /// <param name="gameTime">�Q�[���̏u�ԓI�ȃ^�C�~���O���</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: �����ɃA�b�v�f�[�g�̃R�[�h��ǉ����܂��B
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
        /// �Q�[�� �R���|�[�l���g�����g��`�悷�邽�߂̃��\�b�h�ł��B
        /// </summary>
        /// <param name="gameTime">�Q�[���̏u�ԓI�ȃ^�C�~���O���</param>
        public override void Draw(GameTime gameTime)
        {
            player.Draw(mainCamera);
            enemies.Draw(mainCamera);
            base.Draw(gameTime);
        }
    }
}
