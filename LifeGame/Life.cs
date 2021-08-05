namespace LifeGame
{
    public class Life
    {
        private int[,] map;
        private int[,] sum;
        private int w, h;

        public Life(int w, int h)
        {
            this.w = w;
            this.h = h;
            map = new int [w, h];
            sum = new int[w, h];
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                    map[x, y] = 0;
        }

        public int Turn(int x, int y)
        {
            map[x, y] = map[x, y] == 0 ? 1 : 0;
            return map[x, y];
        }

        public int GetMap(int x, int y)
        {
            if (x < 0 || x >= w)
                return 0;
            if (y < 0 || y >= h)
                return 0;
            
            return map[x, y];
        } 

        private int Around(int x, int y)
        {
            int sum = 0;
            
            for (int sx = -1; sx <= 1; sx++)
            for (int sy = -1; sy <= 1; sy++)
                if (GetMap(x + sx, y + sy) > 0)
                    sum++;
            
            return sum;
        }
        //
        public int GetSum(int x, int y)
        {
            if (x >= w)
                return 0;
            if (y >= h)
                return 0;
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            
            return sum[x, y];
        } 
        private void Prepare()
        {
            for (int x = w - 1; x >= 0; x--)
                for (int y = h - 1; y >= 0; y--)
                {
                    sum[x, y] = GetMap(x, y) + 
                        GetSum(x + 1, y) + 
                        GetSum(x, y + 1) -
                        GetSum(x + 1, y + 1);
                }
        }
        private int AroundFaster(int x, int y)
        {
            return GetSum(x - 1, y - 1) - 
                GetSum(x + 2, y - 1) - 
                GetSum(x - 1, y + 2) + 
                GetSum(x + 2, y + 2);
        }
        //
        
        public void StepOne()
        {
            Prepare();
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                int a = AroundFaster(x, y);
                if (map[x,y] == 1)
                {
                    if (a <= 2) 
                        map[x, y] = 2;
                    if (a >= 5) 
                        map[x, y] = 2;
                }
                else
                {
                    if (a == 3) 
                        map[x, y] = -1;
                }
            }
                    
        }
        public void StepTwo()
        {
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                if (map[x, y] == -1)
                    map[x, y] = 1;
                else if (map[x, y] == 2)
                    map[x, y] = 0;
            }
        }
    }
}