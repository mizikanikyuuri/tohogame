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

namespace GameInterfaces
{
    public delegate void Clicked();
    public delegate void MouseOn();
    enum TEXT_STATE{
        DEFAULT,
        MOUSE_ON,
        MOUSE_CLICK,
    };
    /// <summary>
    /// クリック可能なテキストを扱うためのクラスです。自力で描画まではできないのでspriteBatchを用いて描画してください
    /// </summary>
    public class ClickAbleText
    {
        public event Clicked txtClickedEvent;
        public event MouseOn txtMouseOnEvent;
        private SpriteFont defaultFont, mouseOnFont, mouseClickFont;
        private TEXT_STATE state;
        private string interTxt;
        private Vector2 interPos;
        /// <summary>
        /// 表示するテキストの内容を変更、取得します
        /// </summary>
        public string txt{
            set
            {
                    this.interTxt = value;
            }
            get{
                return this.interTxt;
            }
        }
        /// <summary>
        /// 文字の中心の座標を変更、取得します
        /// </summary>
        public Vector2 centerPos {
            set {
                    this.interPos = value;
            }
            get {
                return this.interPos;
            }
        }
        /// <summary>
        /// 文字の左上端の座標を取得します。変更はできません
        /// </summary>
        public Vector2 pos {
            get {
                Vector2 outPos = this.interPos - GetStateFont().MeasureString(this.interTxt)/2;
                return outPos;
            }
        }
        /// <param name="txt">表示する文章</param>
        /// <param name="defaultFont">テキストの通常時に描画するフォント</param>
        public ClickAbleText(string txt, SpriteFont defaultFont)
        {
            this.defaultFont = defaultFont;
            this.txt = txt;
        }
        /// <param name="txt">表示する文章</param>
        /// <param name="defaultFont">テキストの通常時に描画するフォント</param>
        /// <param name="mouseOnFont">テキストにマウスが乗っているときに描画するフォント</param>
        public ClickAbleText(string txt, SpriteFont defaultFont, SpriteFont mouseOnFont)
        {
            this.defaultFont = defaultFont;
            this.mouseOnFont = mouseOnFont;
            this.txt = txt;
        }
        /// <param name="txt">表示する文章</param>
        /// <param name="defaultFont">テキストの通常時に描画するフォント</param>
        /// <param name="mouseOnFont">テキストにマウスが乗っているときに描画するフォント</param>
        /// <param name="mouseOnFont">テキストをマウスがクリックしたときに描画するフォント</param>
        public ClickAbleText(string txt, SpriteFont defaultFont, SpriteFont mouseOnFont, SpriteFont mouseClickFont)
        {
            this.defaultFont = defaultFont;
            this.mouseOnFont = mouseOnFont;
            this.mouseClickFont = mouseClickFont;
            this.txt = txt;
        }
        /// <summary>
        /// このフレーム、テキストがマウスによってどのような状態にあるか確認します。
        /// またマウスが乗っているとき、マウスによってクリックされた時それぞれのイベントを返します。
        /// </summary>
        /// <param name="mouseState">マウスの状態情報</param>
        public void txtUpdate(MouseState mouseState)
        {
            Vector2 stringSize, mousePos;
            stringSize = GetStateFont().MeasureString(txt);
            mousePos.X = mouseState.X - pos.X;
            mousePos.Y = mouseState.Y - pos.Y;
            if (mousePos.X < 0 || mousePos.X > stringSize.X)
            {
                state = TEXT_STATE.DEFAULT;
                return;
            }
            if (mousePos.Y < 0 || mousePos.Y > stringSize.Y)
            {
                state = TEXT_STATE.DEFAULT;
                return;
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                txtClickedEvent();
                state = TEXT_STATE.MOUSE_CLICK;
            }
            else
            {
                txtMouseOnEvent();
                state = TEXT_STATE.MOUSE_ON;
            }
            return;
        }
        /// <summary>
        /// このフレームのテキストの状態に応じたフォントを返します
        /// </summary>
        public SpriteFont GetStateFont() {
            if (state == TEXT_STATE.DEFAULT)
                return defaultFont;
            if(state == TEXT_STATE.MOUSE_ON&&mouseOnFont!=null)
                return mouseOnFont;
            if (state == TEXT_STATE.MOUSE_CLICK)
            { 
                if( mouseClickFont != null)
                    return mouseClickFont;
                if (mouseOnFont != null)
                    return mouseOnFont;
               }
            return defaultFont;
        }
        /// <summary>
        /// 表示する文字列の内容を返します。
        /// </summary>
        public override string ToString()
        {
            return txt;
        }
    }
}
