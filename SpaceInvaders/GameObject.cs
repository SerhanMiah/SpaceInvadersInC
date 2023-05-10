using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceInvaders
{
	// Game object that will inherit player, enemy and bullet
	public abstract class GameObject
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public ImageSource Image { get; set; }

		public GameObject(double x, double y, double width, double height, ImageSource image)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			Image = image;
		}

		public abstract void Update(double deltaTime);
		public abstract void Render(Canvas canvas);
	}

	public class PlayerShip : GameObject
	{
		public double Speed { get; set; }
		public double BulletCooldown { get; private set; }
		public double BulletCooldownTime { get; set; }

		// inheriting/Contructor 
		public PlayerShip(double x, double y, double width, double height, ImageSource image)
			: base(x, y, width, height, image)
		{
			Speed = 300;
			BulletCooldown = 0;
			BulletCooldownTime = 0.5; // 0.5 seconds
		}

		public void MoveLeft(double deltaTime)
		{
			X -= Speed * deltaTime;
		}

		public void MoveRight(double deltaTime)
		{
			X += Speed * deltaTime;
		}

		public bool CanShoot()
		{
			return BulletCooldown <= 0;
		}

		public void Shoot()
		{
			// Create and fire a bullet here
			BulletCooldown = BulletCooldownTime;
		}

		public override void Update(double deltaTime)
		{
			// Update the bullet cooldown timer
			if (BulletCooldown > 0)
			{
				BulletCooldown -= deltaTime;
			}
		}

		public override void Render(Canvas canvas)
		{
			var playerImage = new Image
			{
				Source = Image,
				Width = Width,
				Height = Height
			};

			Canvas.SetLeft(playerImage, X);
			Canvas.SetTop(playerImage, Y);
			canvas.Children.Add(playerImage);
		}
	}


	public class EnemyShip : GameObject
	{
		public double Speed { get; set; }
		public int Direction { get; set; } // -1 for left, 1 for right
		public double TimeUntilNextMove { get; set; }

		public EnemyShip(double x, double y, double width, double height, ImageSource image)
			: base(x, y, width, height, image)
		{
			Speed = 50;
			Direction = 1;
			TimeUntilNextMove = 0.5;
		}

		public void Move(double deltaTime)
		{
			X += Direction * Speed * deltaTime;
		}

		public void MoveDown()
		{
			Y += Height;
		}

		public void ChangeDirection()
		{
			Direction *= -1;
		}

		public override void Update(double deltaTime)
		{
			TimeUntilNextMove -= deltaTime;

			if (TimeUntilNextMove <= 0)
			{
				Move(deltaTime);
				TimeUntilNextMove = 0.5;
			}
		}

		public override void Render(Canvas canvas)
		{
			var enemyImage = new Image
			{
				Source = Image,
				Width = Width,
				Height = Height
			};

			Canvas.SetLeft(enemyImage, X);
			Canvas.SetTop(enemyImage, Y);
			canvas.Children.Add(enemyImage);
		}

		private List<EnemyShip> CreateEnemyGrid(int rows, int columns, double spacing, double enemyWidth, double enemyHeight)
		{
			var enemyShips = new List<EnemyShip>();

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < columns; col++)
				{
					var x = col * (spacing + enemyWidth);
					var y = row * (spacing + enemyHeight);
					var enemyImage = new BitmapImage(new Uri("Assets/enemy.png", UriKind.Relative));
					var enemy = new EnemyShip(x, y, enemyWidth, enemyHeight, enemyImage);
					enemyShips.Add(enemy);
				}
			}

			return enemyShips;
		}

	}


	public class Bullet : GameObject
	{
		public double Speed { get; set; }
		public int Direction { get; set; } // -1 for up, 1 for down

		public Bullet(double x, double y, double width, double height, ImageSource image, int direction)
			: base(x, y, width, height, image)
		{
			Speed = 400;
			Direction = direction;
		}

		public void Move(double deltaTime)
		{
			Y += Direction * Speed * deltaTime;
		}

		public override void Update(double deltaTime)
		{
			Move(deltaTime);
		}

		public override void Render(Canvas canvas)
		{
			var bulletImage = new Image
			{
				Source = Image,
				Width = Width,
				Height = Height
			};

			Canvas.SetLeft(bulletImage, X);
			Canvas.SetTop(bulletImage, Y);
			canvas.Children.Add(bulletImage);
		}
	}


}
