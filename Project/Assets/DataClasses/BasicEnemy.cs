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
    public class BasicEnemy : Enemy
    {
        private bool isReadyToAttack = true;

        public BasicEnemy(int id, string name, double health, double speed, double damage, double attackSpeed, Vector position, int scoreValue, int goldValue, UserControl userControl)
            : base(id, name, health, speed, damage, attackSpeed, position, scoreValue, goldValue, userControl)
        {
            InitializeTimer();
        }
        public BasicEnemy(BasicEnemy enemy)
            : base(enemy)
        {
            InitializeTimer();
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
        public override void Movement(Vector playerPosition)
        {
            Vector direction = playerPosition - new Vector(Position.X + UserControl.ActualWidth / 2, Position.Y + UserControl.ActualHeight / 2);
            direction.Normalize();

            Position += direction * Speed;
            UserControl.SetValue(Canvas.LeftProperty, Position.X);
            UserControl.SetValue(Canvas.TopProperty, Position.Y);

            RotateEnemyToPlayer(direction);
        }
        private void RotateEnemyToPlayer(Vector direction)
        {
            double angle = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI;
            var rotateTransform = new RotateTransform(angle, UserControl.ActualWidth / 2, UserControl.ActualHeight / 2);
            UserControl.RenderTransform = rotateTransform;
        }
        public override void Attack(Player player)
        {
            if (isReadyToAttack && CheckCollisionWithPlayer())
            {
                player.TakeDamage(Damage);
                isReadyToAttack = false;
                InitializeTimer();
            }
        }
        private bool CheckCollisionWithPlayer()
        {
            var gameScreen = MainWindow.gameControls.GameScreen;
            var playerControl = MainWindow.gameControls.character1Control;

            GeneralTransform enemyTransform = UserControl.TransformToVisual(gameScreen);
            Rect enemyBounds = enemyTransform.TransformBounds(new Rect(0, 0, UserControl.ActualWidth, UserControl.ActualHeight));

            GeneralTransform playerTransform = playerControl.TransformToVisual(gameScreen);
            Rect playerBounds = playerTransform.TransformBounds(new Rect(0, 0, playerControl.ActualWidth, playerControl.ActualHeight));

            return enemyBounds.IntersectsWith(playerBounds);
        }
        public override void UpdateHP()
        {
            BasicEnemyControl control = (BasicEnemyControl)UserControl;
            control.UpdateHP(this.CurrentHealth);
        }
    }
}
