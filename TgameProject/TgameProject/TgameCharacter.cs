using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tgame
{
    public interface TgameCamera{
        Vector3 LookAt
        {
            get;
        }
        Vector3 Location
        {
            get;
        }
        Matrix View{
            get;
        }
        Matrix Projection
        {
            get;
        }

    };
    public class PlayerCamera :TgameCamera{
        Vector3 lookAt;
        Vector3 location;
        Player target;
        float distance;
        double longtitude, ratitude;
        MouseState preState;
        Matrix view;
        Matrix projection;
        public Vector3 LookAt {
            get {
                return lookAt;
            }
        }
        public Vector3 Location {
            get {
                return location;
            }
        }
        public Matrix View{
            get {
                return view;
            }
        }
        public Matrix Projection {
            get {
                return projection;
            }
        }
        public PlayerCamera(Player player,float rad,float aspect) {
            projection = Matrix.CreatePerspectiveFieldOfView(rad, aspect, 1.0f, 1000f);
            distance = 10;
            target = player;
            longtitude = 0;
            ratitude = 0;
        }
        public void Input(MouseState state) {
            int delta_x;
            int delta_y;
            distance = state.ScrollWheelValue;
            delta_x = state.X - preState.X;
            delta_y = state.Y - preState.Y;
            longtitude += delta_x / 10f;
            ratitude += delta_y / 10f;
            location = new Vector3((float)(Math.Cos(ratitude) * Math.Sin(longtitude)),
                                   (float)(Math.Sin(ratitude)),
                                   (float)(Math.Cos(ratitude) * Math.Cos(longtitude)));
            location *= distance*0.01f;
            lookAt = target.Pos;
            view = Matrix.CreateLookAt(
                this.location,
                this.lookAt,
                Vector3.Up
            );
            preState = state;
        }
        public void UpDate() {
            
            
        }
    
    };
    public struct CharactorPalameter {
        public int MaxHitPoint;
        public int MaxMagicPoint;
        public int weight;
        public int MaxSpeed;
        public Vector3 pos;
        private Vector3 dir;
        public Vector3 Dir {
            set {
                dir = Vector3.Normalize(value);
            }
            get {
                return dir;
            }
        }
    };
    public struct EnemyAI { };
    public class Enemies
    {
        EnemyCell enemies;
        private int size;
        public int Size {
            get {
                return size;
            }
        }
        public class EnemyCell
        {
            private EnemyCell nextCell;
            public EnemyCell NextCell
            {
                set
                {
                    nextCell = value;
                }
                get
                {
                    return nextCell;
                }
            }
            private Enemy enemy;
            public Enemy Value {
                set {
                    enemy = value;
                }
                get {
                    return enemy;
                }

            }
            public EnemyCell(Enemy enemy)
            {
                this.enemy = enemy;
            }
            public EnemyCell(Enemy enemy, EnemyCell nextCell)
            {
                this.enemy = enemy;
                this.nextCell = nextCell;
            }
            public EnemyCell CallNext()
            {
                return nextCell;
            }
        };
        public class EnemyEnumrator : IEnumerator<Enemy> {
            private int position;
            private EnemyCell current;
            private EnemyCell firstCell;
            public Tgame.Enemy Current {
                get {
                    if(current != null )
                        return this.current.Value;
                    throw new InvalidOperationException();
                }
            }
            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (current != null)
                        return this.current.Value;
                    throw new InvalidOperationException();
                }
            }
            //public Object IEnumerator<Enemy>.Current
            //{
            //    get
            //    {
            //        return this.Current;
            //    }
            //}
            public EnemyEnumrator(EnemyCell firstCell) {
                position = -1;
                this.firstCell = firstCell;
            }
            public void Dispose() { }
            public bool MoveNext() {
                if (position == -1)
                    current = firstCell;
                else
                {
                    current = current.NextCell;
                }
                if (current == null)
                    return false;
                position++;
                return true;
            }
            public void Reset() {
                position = -1;
                current = null;
            }
        }
        public Enemies() {
            size = 0;
        }
        public void Add(Enemy newEnemy) {
            if (enemies == null)
                enemies = new EnemyCell(newEnemy);
            else
            {
                if (Contains(newEnemy))
                    throw new ArgumentException("you added same enemy for Enemies");
                enemies = new EnemyCell(newEnemy, enemies);
            }
        }
        public void Clear() {
            enemies = null;
        }
        public bool Contains(Enemy contain) {
            EnemyCell nowCell;
            nowCell = enemies;
            while (nowCell.NextCell != null) {
                if (nowCell.Value==contain)
                    return true;
            }
            return false;
        }
        public void CopyTo(Enemies allyEnemies) {
            throw new NotImplementedException("this method is not Implemented. check Enemis CopyTo");
        }
        public IEnumerator<Enemy> GetEnumerator(){
            return new EnemyEnumrator(this.enemies);
        }
        public void Input()
        {
            foreach (Enemy characher in this)
            {
                characher.Input();
            }
        
        }
        public void UpDate()
        {
            foreach (Enemy characher in this)
            {
                characher.UpDate();
            }
        }
        public void Draw(TgameCamera camera)
        {
            foreach (Enemy characher in this)
            {
                characher.Draw(camera);
            }
        }
    };
    public interface TgameObject {
    };
    public abstract class Character:TgameObject
    {
        protected Model model;
        protected CharactorPalameter parameter;
        private Vector3 pos, dir;
        private double xz_rad, yz_rad;
        protected Matrix position;
        public Vector3 Pos {
             protected set {
                pos = value;
            }
             get {
                return pos;
            }
        }
        public Vector3 DirFront {
             protected set{
                dir = Vector3.Normalize(value);
                yz_rad = Math.Asin(value.Y);
                xz_rad = Math.Asin(parameter.Dir.X / Math.Sqrt(parameter.Dir.Z * parameter.Dir.Z + parameter.Dir.X * parameter.Dir.X));
            }
            get {
                return dir;
            }
        }
        protected Vector3 DirRight
        {
            get
            {
                return Vector3.Normalize(new Vector3((float)(Math.Cos(yz_rad) * Math.Cos(xz_rad)),
                             (float)Math.Sin(yz_rad),
                             (float)(Math.Cos(yz_rad) * Math.Sin(xz_rad))));
            }
        }
        protected double yz_Rad{
            set {
                yz_rad = value;
                dir = new Vector3((float)(Math.Cos(yz_rad)*Math.Sin(xz_rad)),
                             (float)Math.Sin(yz_rad),
                             (float)(Math.Cos(yz_rad) * Math.Cos(xz_rad))
                    );
                dir = Vector3.Normalize(dir);
            }
            get {
                return yz_rad;
            }
        }
        protected double xz_Rad
        {
            set
            {
                xz_rad = value;
                dir = new Vector3((float)(Math.Cos(yz_rad) * Math.Sin(xz_rad)),
                             (float)Math.Sin(yz_rad),
                             (float)(Math.Cos(yz_rad) * Math.Cos(xz_rad))
                    );
                dir = Vector3.Normalize(dir);

            }
            get
            {
                return xz_rad;
            }
        }
        public Character(Model model, CharactorPalameter parameter)
        {
            this.model = model;
            this.parameter = parameter;
            pos = new Vector3(parameter.pos.X, parameter.pos.Y, parameter.pos.Z);
            dir = new Vector3(parameter.Dir.X, parameter.Dir.Y, parameter.Dir.Z);
            position = Matrix.CreateWorld(pos, dir, new Vector3(0, 1, 0));
            yz_rad = Math.Asin(parameter.Dir.Y);
            xz_rad = Math.Asin(parameter.Dir.X / Math.Sqrt(parameter.Dir.Z * parameter.Dir.Z + parameter.Dir.X * parameter.Dir.X));
            return ;
        }
        public virtual void Input() {
            position = Matrix.CreateWorld(pos, dir, Vector3.Up);
        }
        public virtual void UpDate()
        {
            return;
        }
        public virtual void Draw(TgameCamera camera)
        {
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // デフォルトのライト適用
                    effect.EnableDefaultLighting();
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.World = position;
                }
            }
            foreach (ModelMesh mesh in this.model.Meshes)
            {
                mesh.Draw();
            }
            return;
        }
    };
    public class Player : Character {
        float speed;
        public Player(Model model, CharactorPalameter parameter)
            : base(model,parameter)
        {
            speed = parameter.MaxSpeed;
        }
        public void Input(KeyboardState keyState)
        {
            float x=0,z=0;
            Vector3 moveVec;
            if (keyState.IsKeyDown(Keys.Up))
            if(keyState.IsKeyDown(Keys.Left))
                x--;
            if(keyState.IsKeyDown(Keys.Right))
                x++;
            if(keyState.IsKeyDown(Keys.Up))
                z++;
            if (keyState.IsKeyDown(Keys.Down))
                z--;
            moveVec = z * DirFront + x * DirRight;
            moveVec = speed * Vector3.Normalize(moveVec);
            base.Input();
        }
        public override void UpDate()
        {
            base.UpDate();
        }
        public override void Draw(TgameCamera camera)
        {
            base.Draw(camera);
        }

    };
    public abstract class Enemy : Character {
        protected Enemy(Model model,CharactorPalameter parameter):base(model,parameter) {

        }
        public override void Input() {
            base.Input();
        }
        public override void UpDate()
        {
            base.UpDate();
        }
        public override void Draw(TgameCamera camera)
        {
            base.Draw(camera);
        }

    
    };
    public class PlaneEnemy : Enemy {
        float speed;
        public PlaneEnemy(Model model, CharactorPalameter parameter) : base(model, parameter) {
            speed = 1;
        }
        public override void Input()
        {
            xz_Rad+=2*Math.PI/360;
            Pos += speed*DirFront;
            base.Input();
        }
        public override void UpDate()
        {
            base.UpDate();
        }
        public override void Draw(TgameCamera camera)
        {
            base.Draw(camera);
        }
    
    };
}
