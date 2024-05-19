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
using System.Windows.Media;

namespace Project.Assets.ControlClasses
{
    public class EnemyControls
    {
        protected SoundControls sound = new SoundControls();
        private List<Enemy> enemies;
        private DispatcherTimer SpawnTimer;
        private DispatcherTimer UpdateTimer;
        private Random random = new Random();

        private double currentDifficultyMultiplier;
        private Canvas GameScreen;

        private int maxEnemies;
        private double timeElapsed;

        private List<Vector> spawnPoints;

        private DispatcherTimer DifficultyIncreaseTimer;

        public EnemyControls(double difficultyMultiplier, Canvas gameScreen)
        {
            enemies = new List<Enemy>();
            SpawnTimer = new DispatcherTimer();
            UpdateTimer = new DispatcherTimer();
            DifficultyIncreaseTimer = new DispatcherTimer();
            GameScreen = gameScreen;
            currentDifficultyMultiplier = difficultyMultiplier;
            maxEnemies = 10;
            timeElapsed = 0;

            spawnPoints = GenerateSpawnPoints();
        }

        public void StartEnemySpawning()
        {
            SpawnTimer.Interval = TimeSpan.FromSeconds(3 + new Random().NextDouble() * 2);
            SpawnTimer.Tick += SpawnTimer_Tick;
            SpawnTimer.Start();

            UpdateTimer.Interval = TimeSpan.FromMilliseconds(16);
            UpdateTimer.Tick += UpdateEnemiesTick;
            UpdateTimer.Start();

            DifficultyIncreaseTimer.Interval = TimeSpan.FromSeconds(20);
            DifficultyIncreaseTimer.Tick += DifficultyIncreaseTimer_Tick;
            DifficultyIncreaseTimer.Start();

            SpawnEnemies(currentDifficultyMultiplier);
        }

        private void DifficultyIncreaseTimer_Tick(object sender, EventArgs e)
        {
            currentDifficultyMultiplier *= 0.95;
        }

        public void StopUpdate()
        {
            SpawnTimer.Stop();
            UpdateTimer.Stop();
            DifficultyIncreaseTimer.Stop();
        }
        public void ResumeUpdate()
        {
            SpawnTimer.Start();
            UpdateTimer.Start();
            DifficultyIncreaseTimer.Start();
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
            sound.PlaySound("enemy-death");
            if (enemy is RangedEnemy rangedEnemy)
            {
                foreach (var bullet in rangedEnemy.Bullets)
                {
                    GameScreen.Children.Remove(bullet.UserControl);
                }
            }
            enemies.Remove(enemy);
            GameScreen.Children.Remove(enemy.UserControl);
            MainWindow.currentScore += enemy.ScoreValue;
            MainWindow.player.Gold += enemy.GoldValue;
        }

        private List<Vector> GenerateSpawnPoints()
        {
            return new List<Vector>
            {
            new Vector(-50, random.NextDouble() * GameScreen.ActualHeight),
            new Vector(GameScreen.ActualWidth + 50, random.NextDouble() * GameScreen.ActualHeight),
            new Vector(random.NextDouble() * GameScreen.ActualWidth, -50),
            new Vector(random.NextDouble() * GameScreen.ActualWidth, GameScreen.ActualHeight + 50)
            };
        }
        private int GetRandomSpawnPointIndex()
        {
            return random.Next(spawnPoints.Count);
        }
        private Vector GetRandomSpawnPoint()
        {
            int index = GetRandomSpawnPointIndex();
            return spawnPoints[index];
        }
        private BasicEnemy CreateBasicEnemy()
        {
            Vector spawnPoint = GetRandomSpawnPoint();
            return new BasicEnemy(1, "Basic Enemy", 20 / currentDifficultyMultiplier, 2 / currentDifficultyMultiplier, 5 / currentDifficultyMultiplier, 1 * currentDifficultyMultiplier, spawnPoint, (int)(10.0 / currentDifficultyMultiplier), 10, new BasicEnemyControl(20 / currentDifficultyMultiplier));
        }
        private RangedEnemy CreateRangedEnemy()
        {
            Vector spawnPoint = GetRandomSpawnPoint();
            return new RangedEnemy(2, "Ranged Enemy", 10 / currentDifficultyMultiplier, 1 / currentDifficultyMultiplier, 3 / currentDifficultyMultiplier, 1.5 * currentDifficultyMultiplier, spawnPoint, (int)(30.0 / currentDifficultyMultiplier), 20, new RangedEnemyControl(10 / currentDifficultyMultiplier));
        }

        private void SpawnTimer_Tick(object sender, EventArgs e)
        {
            SpawnTimer.Stop();
            double randomDelay = 2 + random.NextDouble() * 4;
            SpawnTimer.Interval = TimeSpan.FromSeconds(randomDelay);
            SpawnTimer.Start();

            spawnPoints = GenerateSpawnPoints();

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

            Vector playerPosition = new Vector(MainWindow.player.Position.X, MainWindow.player.Position.Y);

            foreach (var enemy in enemies.ToList())
            {
                enemy.UpdateHP();
                if (enemy.CurrentHealth <= 0)
                {
                    EnemyDeath(enemy);
                }
                else
                {
                    enemy.Movement(playerPosition);
                    enemy.Attack(MainWindow.player);
                    if (enemy is RangedEnemy rangedEnemy)
                    {
                        rangedEnemy.UpdateBullets(MainWindow.player);
                    }
                }
            }
            CheckCollisions();
        }
        private void CheckCollisions()
        {
            foreach (var enemy in enemies.ToList())
            {
                var enemyBounds = GetBoundsRelativeToGameScreen(enemy.UserControl);

                foreach (var bullet in MainWindow.player.Bullets.ToList())
                {
                    var bulletBounds = GetBoundsRelativeToGameScreen(bullet.UserControl);

                    if (enemyBounds.IntersectsWith(bulletBounds))
                    {
                        enemy.CurrentHealth -= MainWindow.player.Damage;

                        MainWindow.player.Bullets.Remove(bullet);
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
        private Rect GetBoundsRelativeToGameScreen(FrameworkElement element)
        {
            var gameScreen = GameScreen;
            GeneralTransform transform = element.TransformToVisual(gameScreen);
            return transform.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
        }
    }
}