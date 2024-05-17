using Project.Assets.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Project.Assets.UserControls;
using System.Windows.Documents;

namespace Project.Assets.ControlClasses
{
    public class EnemyControls
    {
        private List<Enemy> enemies;
        private DispatcherTimer SpawnTimer;
        private DispatcherTimer UpdateTimer;
        private Random random = new Random();

        private double currentDifficultyMultiplier;
        private Canvas GameScreen;

        private int maxEnemies;
        private double timeElapsed;

        public EnemyControls(double difficultyMultiplier, Canvas gameScreen)
        {
            enemies = new List<Enemy>();
            SpawnTimer = new DispatcherTimer();
            UpdateTimer = new DispatcherTimer();
            GameScreen = gameScreen;
            currentDifficultyMultiplier = difficultyMultiplier;
            maxEnemies = 10;
            timeElapsed = 0;
        }

        public void StartEnemySpawning()
        {
            SpawnTimer.Interval = TimeSpan.FromSeconds(3 + new Random().NextDouble() * 2);
            SpawnTimer.Tick += SpawnTimer_Tick;
            SpawnTimer.Start();

            UpdateTimer.Interval = TimeSpan.FromMilliseconds(16);
            UpdateTimer.Tick += UpdateEnemiesTick;
            UpdateTimer.Start();
        }

        private void SpawnEnemies(double difficultyMultiplier)
        {
            double requiredEnemies = maxEnemies / currentDifficultyMultiplier;
            if (enemies.Count < (int)requiredEnemies)
            {
                int randomEnemyType = random.Next(2);
                Enemy enemy;

                if (randomEnemyType == 0)
                {
                    enemy = CreateBasicEnemy();
                }
                else
                {
                    enemy = CreateRangedEnemy();
                }

                enemies.Add(enemy);
                GameScreen.Children.Add(enemy.UserControl);
            }
        }
        private void EnemyDeath(Enemy enemy)
        {
            enemies.Remove(enemy);
            GameScreen.Children.Remove(enemy.UserControl);
            MainWindow.currentScore += enemy.ScoreValue;
            MainWindow.charapter1.Gold += enemy.GoldValue;
        }
        private BasicEnemy CreateBasicEnemy()
        {
            return new BasicEnemy(1, "Basic Enemy", 20 * currentDifficultyMultiplier, 2 * currentDifficultyMultiplier, 5 * currentDifficultyMultiplier, 1 * currentDifficultyMultiplier, new Vector(0, 0), (int)(10.0 / currentDifficultyMultiplier), 10, new BasicEnemyControl());
        }
        private RangedEnemy CreateRangedEnemy()
        {
            return new RangedEnemy(2, "Ranged Enemy", 10 * currentDifficultyMultiplier, 1 * currentDifficultyMultiplier, 3 * currentDifficultyMultiplier, 0.5 * currentDifficultyMultiplier, new Vector(0, 0), (int)(30.0 / currentDifficultyMultiplier), 20, new RangedEnemyControl());
        }
        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnTimer.Stop();
            double randomDelay = 1 + random.NextDouble() * 4;
            SpawnTimer.Interval = TimeSpan.FromSeconds(randomDelay);
            SpawnTimer.Start();
            SpawnEnemies(currentDifficultyMultiplier);
        }

        public void UpdateEnemiesTick(object sender, EventArgs e)
        {
            timeElapsed += UpdateTimer.Interval.TotalSeconds;
            if (timeElapsed >= 10)
    {
                maxEnemies++;
                timeElapsed = 0;
            }

            foreach (var enemy in enemies.ToList())
            {
                enemy.Movement();
                enemy.Attack();
            }

            CheckCollisions();
        }
        private void CheckCollisions()
        {
            foreach (var enemy in enemies.ToList())
            {
                var enemyPosition = enemy.UserControl.TransformToAncestor(GameScreen)
                    .Transform(new Point(0, 0));
                var enemyBounds = new Rect(enemyPosition, new Size(enemy.UserControl.ActualWidth, enemy.UserControl.ActualHeight));

                foreach (var bullet in MainWindow.charapter1.Bullets.ToList())
                {
                    var bulletPosition = bullet.UserControl.TransformToAncestor(GameScreen).Transform(new Point(0, 0));
                    var bulletBounds = new Rect(bulletPosition, new Size(bullet.UserControl.ActualWidth, bullet.UserControl.ActualHeight));

                    if (enemyBounds.IntersectsWith(bulletBounds))
                    {
                        enemy.CurrentHealth -= MainWindow.charapter1.Damage;

                        MainWindow.charapter1.Bullets.Remove(bullet);
                        GameScreen.Children.Remove(bullet.UserControl);

                        if (enemy.CurrentHealth <= 0)
                        {
                            EnemyDeath(enemy);
                        }

                        break;
                    }
                }
            }
        }
    }
}