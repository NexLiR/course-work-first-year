using Project.Assets.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Project.Assets.DataClasses
{
    public class RangedEnemy : Enemy
    {
        private bool isReadyToAttack = true;
		MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
		public List<Bullet> Bullets { get; set; } = new List<Bullet>();
        public RangedEnemy(int id, string name, double health, double speed, double damage, double attackSpeed, Vector position, int scoreValue, int goldValue, UserControl userControl)
            : base(id, name, health, speed, damage, attackSpeed, position, scoreValue, goldValue, userControl)
        {
            InitializeTimer();
        }
        public RangedEnemy(RangedEnemy enemy)
            : base(enemy)
        {
            InitializeTimer();
        }
        public override void Movement(Vector playerPosition)
        {
            Vector directionToPlayer = playerPosition - new Vector(Position.X + UserControl.ActualWidth / 2, Position.Y + UserControl.ActualHeight / 2);
            directionToPlayer.Normalize();

            if ((playerPosition - Position).Length > 300)
            {
                Position += directionToPlayer * Speed;
            }
            RotateEnemyToPlayer(directionToPlayer);
            UserControl.SetValue(Canvas.LeftProperty, Position.X);
            UserControl.SetValue(Canvas.TopProperty, Position.Y);
        }
        private void RotateEnemyToPlayer(Vector direction)
        {
            double angle = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI;
            var rotateTransform = new RotateTransform(angle, UserControl.ActualWidth / 2, UserControl.ActualHeight / 2);
            UserControl.RenderTransform = rotateTransform;
        }
        private void InitializeTimer()
        {
            DispatcherTimer attackTimer = new DispatcherTimer();
            attackTimer.Tick += (sender, e) =>
            {
                isReadyToAttack = true;
                attackTimer.Stop();
            };
            attackTimer.Interval = TimeSpan.FromSeconds(AttackSpeed);
            attackTimer.Start();
        }

        public override void Attack(Player player)
        {
            if (isReadyToAttack && (mainWindow.player.Position - Position).Length <= 301)
        {
                var characterPosition = Position;
                var direction = player.Position - characterPosition;
                direction.Normalize();

                var bullet = CreateProjectile(new Point(characterPosition.X, characterPosition.Y), direction);

				mainWindow.gameControls.GameScreen.GameSpace.Children.Add(bullet.UserControl);
                bullet.UserControl.Visibility = Visibility.Visible;
                Bullets.Add(bullet);

                isReadyToAttack = false;
                InitializeTimer();
            }
        }

        public void UpdateBullets(Player player)
        {
            foreach (var bullet in Bullets.ToList())
            {
                bullet.Position = new Point(bullet.Position.X + bullet.Direction.X * bullet.Speed, bullet.Position.Y + bullet.Direction.Y * bullet.Speed);
                bullet.UserControl.SetValue(Canvas.LeftProperty, bullet.Position.X);
                bullet.UserControl.SetValue(Canvas.TopProperty, bullet.Position.Y);

                bullet.LifeTime -= 10;
                if (bullet.LifeTime <= 0)
                {
					mainWindow.gameControls.GameScreen.GameSpace.Children.Remove(bullet.UserControl);
                    Bullets.Remove(bullet);
                }
                else if (CheckCollisionWithPlayer(bullet, player))
                {
					mainWindow.gameControls.GameScreen.GameSpace.Children.Remove(bullet.UserControl);
                    Bullets.Remove(bullet);
                    player.TakeDamage(Damage);
                }
            }
        }

        private Bullet CreateProjectile(Point position, Vector direction)
        {
            return new Bullet(position, direction, 7.5, 400, Damage, new BulletControl());
        }

        private bool CheckCollisionWithPlayer(Bullet bullet, Player player)
        {
            var gameScreen = mainWindow.gameControls.GameScreen.GameSpace;
            var playerControl = mainWindow.gameControls.playerControl;

            GeneralTransform bulletTransform = bullet.UserControl.TransformToVisual(gameScreen);
            Rect bulletBounds = bulletTransform.TransformBounds(new Rect(0, 0, bullet.UserControl.ActualWidth, bullet.UserControl.ActualHeight));

            GeneralTransform playerTransform = playerControl.TransformToVisual(gameScreen);
            Rect playerBounds = playerTransform.TransformBounds(new Rect(0, 0, playerControl.ActualWidth, playerControl.ActualHeight));

            return bulletBounds.IntersectsWith(playerBounds);
        }
        public override void UpdateHP()
        {
            RangedEnemyControl control = (RangedEnemyControl)UserControl;
            control.UpdateHP(this.CurrentHealth);
        }
    }
}
