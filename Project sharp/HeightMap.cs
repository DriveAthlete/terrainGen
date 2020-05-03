using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Project_sharp
{
    /// <summary>
    /// Координата в сетке высот 
    /// </summary>
    public struct Coordinate
    {
        /// <summary>
        /// Инициализирует новый экземпляр в  <see cref="T:MidnightBlue.Engine.Coordinate"/> структуре.
        /// </summary>
        /// <param name="x">Координата Х.</param>
        /// <param name="y">Координата Y.</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Координата X
        /// </summary>
        /// <value>Координата X</value>
        public int X { get; set; }

        /// <summary>
        /// Координата X
        /// </summary>
        /// <value>Координата Y</value>
        public int Y { get; set; }
    }

    /// <summary>
    /// Создает фрактальную 2D карту высот, используя алгоритм Diamond Square
    /// </summary>
    public class DiamondSquare
    {
        /// <summary>
        /// Размер карты
        /// </summary>
        private int _size;
        /// <summary>
        /// Зерно, используемое для генерации карты
        /// </summary>
        private int _seed;
        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private Random _rand;
        /// <summary>
        /// Сгенерированные значения карты
        /// </summary>
        private double[,] _values;

        /// <summary>
        /// Инициализация значений и генератора случайных чисел.
        /// </summary>
        /// <param name="size">Размер карты (степень двойки)</param>
        /// <param name="willWrap">Установите в true, чтобы разрешить обтекание фрактала для склеивания</param>
        private void Initialize(int size, bool willWrap)
        {
            _size = size;
            _rand = new Random(_seed);
            _values = new double[_size, _size];

            if (willWrap)
            {
                _values[0, 0]
                  = _values[size - 1, 0]
                  = _values[size - 1, size - 1]
                  = _values[0, size - 1]
                  = -1;
                _values[size / 2, size / 2] = 1;
            }
            else
            {
                _values[0, 0] = RandomDouble();
                _values[size - 1, 0] = RandomDouble();
                _values[size - 1, size - 1] = RandomDouble();
                _values[0, size - 1] = RandomDouble();
            }
        }

        /// <summary>
        /// Инициализация нового экземпляра<see cref="T:MidnightBlue.Engine.DiamondSquare"/> класса.
        /// Текущее время используется как зерно.
        /// </summary>
        /// <param name="size">Размер карты - <b>должен быть степенью двойки</b>.</param>
        /// <param name="willWrap">Установите в true, чтобы разрешить обтекание фрактала для склеивания</param>
        public DiamondSquare(int size, bool willWrap)
        {
            if (!IsPowerOfTwo(size))
            {
                throw new ArgumentOutOfRangeException(nameof(size), "'" + size + "' is not a power of 2");
            }

            _seed = (int)DateTime.Now.Ticks;
            Initialize(size, willWrap);
        }

        /// <summary>
        /// Инициализация нового экземпляра<see cref="T:MidnightBlue.Engine.DiamondSquare"/> класса
        /// </summary>
        /// <param name="size">Размер карты - <b>должен быть степенью двойки</b>.</param>
        /// <param name="willWrap">Установите в true, чтобы разрешить обтекание фрактала для склеивания</param>
        /// <param name="seed">Зерно, используемое для генерации карты</param>
        public DiamondSquare(int size, bool willWrap, int seed)
        {
            if (!IsPowerOfTwo(size))
            {
                throw new ArgumentOutOfRangeException(nameof(size), "'" + size + "' is not a power of 2");
            }

            _seed = seed;
            Initialize(size, willWrap);
        }

        /// <summary>
        /// Получает значение из сгенерированной карты.
        /// </summary>
        /// <returns>Сгенерированное значение.</returns>
        /// <param name="x">The x coordinate to get - wraps if larger or smaller than the map size.</param>
        /// <param name="y">The y coordinate to get  - wraps if larger or smaller than the map size.</param>
        public double GetValue(int x, int y)
        {
            var pos = WrapGrid(x, y, _size, _size);
            return _values[pos.X, pos.Y];
        }

        /// <summary>
        /// генерирует фрактальную карту Diamond-Square (Random Midpoint Displacement).
        /// </summary>
        public void Generate(int featureSize)
        {
            var instanceSize = featureSize;
            var scale = 1.0;

            while (instanceSize > 1)
            {

                Iterate(instanceSize, scale);

                instanceSize /= 2;
                scale /= 2.0;
            }
        }

        /// <summary>
        /// Возращает случайное double значение в диапозоне от -1 до 1
        /// </summary>
        /// <returns>Случайное double значение.</returns>
        private double RandomDouble()
        {
            return _rand.NextDouble() * 2 - 1;
        }

        /// <summary>
        /// Одна итерация Diamond Square
        /// </summary>
        /// <param name="step">Значение шага в текущей итерации</param>
        /// <param name="currentScale">Размер текущего значения в процессе</param>
        private void Iterate(int step, double currentScale)
        {
            var halfStep = step / 2;

            // Handle squares
            for (int y = halfStep; y < halfStep + _size; y += step)
            {
                for (int x = halfStep; x < halfStep + _size; x += step)
                {
                    HandleSquare(x, y, step, RandomDouble() * currentScale);
                }
            }

            // Handle diamonds
            for (int y = 0; y < _size; y += step)
            {
                for (int x = 0; x < _size; x += step)
                {
                    HandleDiamond(x + halfStep, y, step, RandomDouble() * currentScale);
                    HandleDiamond(x, y + halfStep, step, RandomDouble() * currentScale);
                }
            }
        }

        /// <summary>
        /// Обрабатывает square step. Считает сумму точек квадрата равноудаленных
        /// от центра квадрата и присваивает серединной точке новое значение
        /// </summary>
        /// <param name="x">Координата Х для обработки</param>
        /// <param name="y">Координата У для обработки</param>
        /// <param name="step">Текущий шаг</param>
        /// <param name="newValue">Новое значение для просчета</param>
        private void HandleSquare(int x, int y, int step, double newValue)
        {
            var halfStep = step / 2;

            var a = GetValue(x - halfStep, y - halfStep);
            var b = GetValue(x + halfStep, y - halfStep);
            var c = GetValue(x - halfStep, y + halfStep);
            var d = GetValue(x + halfStep, y + halfStep);

            var pos = WrapGrid(x, y, _size, _size);
            _values[pos.X, pos.Y] = ((a + b + c + d) / 4.0) + newValue;
        }

        /// <summary>
        /// Обрабатывае diamond. Суммирует точки ромба, вокруг серединной точки
        /// и присваивает это значение серединной точке
        /// </summary>
        /// <param name="x">Координата Х для обработки</param>
        /// <param name="y">Координата У для обработки</param>
        /// <param name="step">Текущий шаг</param>
        /// <param name="newValue">Новое значение для просчета</param>
        private void HandleDiamond(int x, int y, int step, double newValue)
        {
            var halfStep = step / 2;

            var b = GetValue(x + halfStep, y);
            var d = GetValue(x - halfStep, y);
            var a = GetValue(x, y - halfStep);
            var c = GetValue(x, y + halfStep);

            var pos = WrapGrid(x, y, _size, _size);
            _values[pos.X, pos.Y] = ((a + b + c + d) / 4.0) + newValue;
        }

        /// <summary>
        /// узнает, является ли текущее значение степенью двойки
        /// </summary>
        /// <returns><c>true</c>, если степень двойки, <c>false</c> если нет.</returns>
        /// <param name="val">Значение для проверки.</param>
        private bool IsPowerOfTwo(int val)
        {
            return (val != 0) && (val & (val - 1)) == 0;
        }

        public Coordinate WrapGrid(int x, int y, int width, int height)
        {
            var xResult = 0;
            var yResult = 0;

            if (x >= 0)
            {
                xResult = x % width; // wrap right
            }
            else
            {
                xResult = (width + x % width) % width; // wrap left
            }

            if (y >= 0)
            {
                yResult = y % height; // wrap down
            }
            else
            {
                yResult = (height + y % height) % height; // wrap up
            }

            return new Coordinate(xResult, yResult);
        }

        /// <summary>
        /// Получает значение размера карты
        /// </summary>
        /// <value>Размер карты.</value>
        public int Size
        {
            get { return _size; }
        }
    }
}
