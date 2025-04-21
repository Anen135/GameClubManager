namespace Tasks {
    class Point {
        public double[] coords = { 0, 0, 0 }; // x, y, z
        public Point(double x, double y, double z) {
            coords = [x, y, z];
        }
        public double distance(double[] coords) {
            return Math.Sqrt(Math.Pow(coords[0] - this.coords[0], 2) + Math.Pow(coords[1] - this.coords[1], 2) + Math.Pow(coords[2] - this.coords[2], 2));
        }
        public (double, double) projection(double d = 2) {
            double denominator = coords[2] + d;
            if (denominator == 0) return (-1, -1);
            double x = coords[0] / denominator;
            double y = coords[1] / denominator;
            return (x, y);
        }
    }

    abstract class Figure {
        public Point[] points;
        protected Figure(Point[] points) {
            this.points = points;
        }
        abstract public void print_info();
        abstract public void translate(double x, double y, double z);
        abstract public void rotate(double angleX, double angleY, double angleZ);
        abstract public Point[] copy_points();
    }

    class Cube : Figure {
        public Cube(Point[] points) : base(points) { }
        public override void print_info() {
            Console.WriteLine("Cube projection\t\t| Cube points");
            for (System.Int32 i = 0; i < points.Length; i++) {
                Console.WriteLine($"Point {i + 1}: {points[i].projection()}\t| {points[i].coords[0],2}, {points[i].coords[1],2}, {points[i].coords[2],2}");
            }
        }

        public override Point[] copy_points() {
            Point[] new_points = new Point[points.Length];
            for (System.Int32 i = 0; i < points.Length; i++) {
                new_points[i] = new Point(points[i].coords[0], points[i].coords[1], points[i].coords[2]);
            }
            return new_points;
        }

        public override void translate(double x, double y, double z) {
            for (System.Int32 i = 0; i < points.Length; i++) {
                points[i].coords[0] += x;
                points[i].coords[1] += y;
                points[i].coords[2] += z;
            }
        }
        public override void rotate(double angleX, double angleY, double angleZ) {
            double centerX = 0, centerY = 0, centerZ = 0;
            foreach (var p in points) {
                centerX += p.coords[0];
                centerY += p.coords[1];
                centerZ += p.coords[2];
            }
            centerX /= points.Length;
            centerY /= points.Length;
            centerZ /= points.Length;

            double cosX = Math.Cos(angleX), sinX = Math.Sin(angleX);
            double cosY = Math.Cos(angleY), sinY = Math.Sin(angleY);
            double cosZ = Math.Cos(angleZ), sinZ = Math.Sin(angleZ);

            // Объединённая матрица вращения R = R_Z * R_Y * R_X
            double[,] R = {
                { cosY * cosZ, cosY * sinZ, -sinY },
                { sinX * sinY * cosZ - cosX * sinZ, sinX * sinY * sinZ + cosX * cosZ, sinX * cosY },
                { cosX * sinY * cosZ + sinX * sinZ, cosX * sinY * sinZ - sinX * cosZ, cosX * cosY }
            };
            for (System.Int32 i = 0; i < points.Length; i++) {
                double x = points[i].coords[0] - centerX;
                double y = points[i].coords[1] - centerY;
                double z = points[i].coords[2] - centerZ;
                /*
                 dx =x⋅(cos(t)_Y ⋅ cos(t)_Z)+y⋅(cos(t)_Y​⋅sin(t)_Z)+z⋅(−sin(t)_Y)
                 dy =x⋅(sin(t)_X ⋅ sin(t)_Y ⋅ cos(t)_Z−cos(t)_X⋅sin(t)_Z)+y⋅(sin(t)_X ⋅ sin(t)_Y ⋅ sin(t)_Z+cos(t)_X ⋅ cos(t)_Z)+z⋅(sin(t)_X⋅cos(t)_Y)
                 dz =x⋅(sin(t)_X ⋅ sin(t)_Y ⋅ cos(t)_Z​ +cos(t)_X​ sin(t)_Z​ )+y⋅(cos(t)_X​ ⋅ sin(t)_Y​ ⋅ sin(t)_Z​ - sin(t)_X​ cos(t)_Z​)+z⋅(cos(t)_X​⋅cos(t)_Y​)
                */
                points[i].coords[0] = x * R[0, 0] + y * R[0, 1] + z * R[0, 2] + centerX;
                points[i].coords[1] = x * R[1, 0] + y * R[1, 1] + z * R[1, 2] + centerY;
                points[i].coords[2] = x * R[2, 0] + y * R[2, 1] + z * R[2, 2] + centerZ;
            }
        }

    }
    class Task6 {

        public Task6() {
            System.Int32[] screen_size = { 80, 40 }; // x, y
            System.Int32 px = 0, py = 0;
            Figure cube = new Cube([
                new Point(-1, -1, -1),
                new Point(1, -1, -1),
                new Point(1, 1, -1),
                new Point(-1, 1, -1),
                new Point(-1, -1, 1),
                new Point(1, -1, 1),
                new Point(1, 1, 1),
                new Point(-1, 1, 1)
            ]);
            Point[] figure_buffer;
            cube.print_info();
            Console.WriteLine("Screen coords:");
            for (System.Int32 i = 0; i < cube.points.Length; ++i) {
                transform_to_screen_coords(cube.points[i].projection(), screen_size, out px, out py);
                Console.WriteLine($"Point {i + 1} screen coords: {px}, {py}");
            }
            System.Console.ReadKey();
            System.Console.CursorVisible = false;
            
            cube.translate(0, 0, 0.1);
            while (true) {
                figure_buffer = cube.copy_points();
                cube.rotate(0.1, 0.15, 0.1);
                draw_cube_edges(cube.points, figure_buffer, screen_size, 70, 0);
                //System.Console.ReadKey();
                System.Threading.Thread.Sleep(0);


            }

        } // methods


        static void cube_control(Figure cube, Point[] buffer, System.Int32[] screen_size) {
            if (Console.KeyAvailable) {
                switch (Console.ReadKey(true).Key) {
                    case ConsoleKey.Escape:
                    return;
                    case ConsoleKey.W:
                    cube.translate(0, 0.1, 0);
                    break;
                    case ConsoleKey.S:
                    cube.translate(0, -0.1, 0);
                    break;
                    case ConsoleKey.D:
                    cube.translate(0.1, 0, 0);
                    break;
                    case ConsoleKey.A:
                    cube.translate(-0.1, 0, 0);
                    break;
                    case ConsoleKey.Q:
                    cube.translate(0, 0, -0.1);
                    break;
                    case ConsoleKey.E:
                    cube.translate(0, 0, 0.1);
                    break;
                    case ConsoleKey.LeftArrow:
                    cube.rotate(0, 0.1, 0);
                    break;
                    case ConsoleKey.RightArrow:
                    cube.rotate(0, -0.1, 0);
                    break;
                    case ConsoleKey.UpArrow:
                    cube.rotate(0.1, 0, 0);
                    break;
                    case ConsoleKey.DownArrow:
                    cube.rotate(-0.1, 0, 0);
                    break;

                }
                draw_cube_edges(cube.points, buffer, screen_size, 70, 0);
            }
        }

        static void transform_to_screen_coords((double, double) coords, System.Int32[] screen_size, out System.Int32 sx, out System.Int32 sy) {
            sx = (System.Int32)((coords.Item1 / 2 + 1) * screen_size[0] / 2);
            sy = (System.Int32)((1 - coords.Item2 / 2) * screen_size[1] / 2);
        }

        static void draw_vertices(Figure cube, System.Int32[] screen_size, bool clear = false) {
            float xpadding = 1f;
            int ypadding = 0;
            System.Int32 px = 0, py = 0;
            for (System.Int32 j = 0; j < cube.points.Length; ++j) {
                transform_to_screen_coords(cube.points[j].projection(), screen_size, out px, out py);
                px += (int)(screen_size[0] * xpadding);
                py -= (int)(screen_size[1] * ypadding);
                if (px < 0 || py < 0 || px >= Console.BufferWidth || py >= Console.BufferHeight) continue;
                System.Console.SetCursorPosition(px, py);
                if (clear) System.Console.Write(" ");
                else System.Console.Write("#");
            }
        }

        static void draw_cube_edges(Point[] cube, Point[] buffer, System.Int32[] screen_size, int dx=0, int dy=0) {
            int[,] edges = {
                { 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 }, // Нижняя грань
                { 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 4 }, // Верхняя грань
                { 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }  // Вертикальные рёбра
            };

            for (int i = 0; i < edges.GetLength(0); ++i) {
                (double, double) p21_proj = buffer[edges[i, 0]].projection();
                (double, double) p22_proj = buffer[edges[i, 1]].projection();
                int x21, y21, x22, y22;
                transform_to_screen_coords(p21_proj, screen_size, out x21, out y21);
                transform_to_screen_coords(p22_proj, screen_size, out x22, out y22);
                draw_edges(x21, x22, y21, y22, ' ', dx, dy);
            }

            for (int i = 0; i < edges.GetLength(0); i++) {
                (double, double) p1_proj = cube[edges[i, 0]].projection();
                (double, double) p2_proj = cube[edges[i, 1]].projection();
                int x1, y1, x2, y2;
                transform_to_screen_coords(p1_proj, screen_size, out x1, out y1);
                transform_to_screen_coords(p2_proj, screen_size, out x2, out y2);
                draw_edges(x1, x2, y1, y2, '*', dx, dy);
            }

            //for (int i = 0; i < edges.GetLength(0); i++) {
            //    (double, double) p1_proj = cube[edges[i, 0]].projection();
            //    (double, double) p2_proj = cube[edges[i, 1]].projection();
            //    (double, double) p21_proj = buffer[edges[i,0]].projection();
            //    (double, double) p22_proj = buffer[edges[i, 1]].projection();


            //    int x1, y1, x2, y2;
            //    int x21, y21, x22, y22;
            //    transform_to_screen_coords(p1_proj, screen_size, out x1, out y1);
            //    transform_to_screen_coords(p2_proj, screen_size, out x2, out y2);
            //    transform_to_screen_coords(p21_proj,screen_size,out x21, out y21);
            //    transform_to_screen_coords(p22_proj, screen_size, out x22, out y22);
            //    draw_edges(x21, x22, y21, y22, ' ', dx, dy);
            //    draw_edges(x1, x2, y1, y2, '#', dx, dy);
            //}
        }

        static void draw_edges(int x2, int x1, int y2, int y1, char ch = '#', int sdx=0, int sdy=0){
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = x1 < x2 ? 1 : -1; // Направление движения по x
            int sy = y1 < y2 ? 1 : -1; // Направление движения по y
            int err = dx - dy; // Начальная ошибка

            while (true) {
                if (x1 + sdx < 0 || y1 + sdy < 0 || x1 + sdx >= Console.BufferWidth || y1 + sdy >= Console.BufferHeight) break;
                Console.SetCursorPosition(x1+sdx, y1+sdy);
                Console.Write(ch);

                if (x1 == x2 && y1 == y2) break; // Если достигли конечной точки, выходим

                int e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x1 += sx; } // Смещаемся по x
                if (e2 < dx) { err += dx; y1 += sy; } // Смещаемся по y
            }
        } // methods
    } // classes
} // namespace 
