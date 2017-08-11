using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    class Program
    {
        static void Main(string[] args)
        {
            bool bExit = false; //退出标识
            int ptCounts = 0; //点计数器
            int lnCounts = 0; //线计数器
            ArrayList points = new ArrayList(); //点对象数组
            List<CLine> lines = new List<CLine>(); //线对象数组
            //输入一组点，组成线，输入格式为：x y，当输入字符为“;”时，表示结束一条线的输入，当输入字符为“#”时表示结束所有输入
            Console.WriteLine("Please input points' coordination one by one,the format is x y");
            do
            {
                string instr = Console.ReadLine();//读入坐标值
                if (instr.IndexOf(";") >= 0 && points.Count > 1)//发现“;”,用已输入的坐标值产生新的线
                {
                    int m = points.Count;//已有点数
                    CLine ln = new CLine();//产生新的线对象
                    ln.POINTS = new CPoint[m];//为线对象的点数组分配空间
                    for (int n = 0; n < m; n++)
                    {
                        ln.POINTS[n] = (CPoint)points[n];//为每个点赋值
                    }
                    ln.SetValidation();//设置有效性
                    ln.ID = lnCounts++;
                    lines.Add(ln);//把线对象加入列表
                    points.Clear();//清空点数组对象
                }
                else if (instr.IndexOf("#") >= 0)//结束输入
                {
                    bExit = true;
                }
                else
                {
                    string[] ts = instr.Split(' ');//分解输入字符串
                    double x = double.Parse(ts[0]);//取得x
                    double y = double.Parse(ts[1]);//取得y
                    CPoint p = new CPoint(x, y);//产生新的点对象
                    p.ID = ptCounts++;
                    points.Add(p);//将点加入点数组中
                }
            } while (!bExit);
            bExit = false;
            Console.WriteLine("------Options------");
            Console.WriteLine(@"1 表示打印信息
2 表示比较两条线要素
3 表示两条线相加
4 表示移动线
5 表示插入点
6 表示删除点");
            Console.WriteLine("-------------------");
            do
            {
                Console.Write("Please choose one business: ");
                string key = Console.ReadLine();
                switch (key)
                {
                    case "1":
                        for (int i = 0; i < lines.Count; i++)
                        {
                            Console.WriteLine("line {0} contains {1} points and {2} meters long",
                            i + 1, lines[i].POINTS.Length, lines[i].GetLineLength());
                            for (int j = 0; j < lines[i].POINTS.Length; j++)
                            {
                                Console.WriteLine("({0},{1})", lines[i].POINTS[j].PX, lines[i].POINTS[j].PY);
                            }
                        }
                        break;
                    case "2":
                        if (lines[0].isValid && lines[1].isValid)
                        {
                            if (lines[0] > lines[1])
                                Console.WriteLine("The first line is longer than the second one");
                            else
                                Console.WriteLine("The first line is not longer than the second one");
                        }
                        break;
                    case "3":
                        Console.Write("Please choose the first line's number: ");
                        int numLn1 = int.Parse(Console.ReadLine());
                        Console.Write("Please choose the first line's number: ");
                        int numLn2 = int.Parse(Console.ReadLine());
                        if ((lines[numLn1 - 1] + lines[numLn2 - 1]) != null)
                            lines.Add(lines[numLn1 - 1] + lines[numLn2 - 1]);
                        else
                            Console.WriteLine("ERROR!");
                        break;
                    case "4":
                        Console.Write("Please choose the line's number: ");
                        int numLn = int.Parse(Console.ReadLine()) - 1;
                        Console.Write(@"1 Select Center Point to Move to
2 Select one Point to Move to
Please select the choice:");
                        string myChoice = Console.ReadLine();
                        switch (myChoice)
                        {
                            case "1":
                                Console.Write("Input your destination points: ");
                                string destinationPt1 = Console.ReadLine();
                                string[] dp1 = destinationPt1.Split(' ');//分解输入字符串
                                double x1 = double.Parse(dp1[0]);//取得x
                                double y1 = double.Parse(dp1[1]);//取得y
                                CPoint p1 = new CPoint(x1, y1);//产生新的点对象
                                lines[numLn].Moveto(p1);
                                break;
                            case "2":
                                Console.Write("Input the point's index you choose: ");
                                int selectedIndex = int.Parse(Console.ReadLine());
                                Console.Write("Input your destination points: ");
                                string destinationPt2 = Console.ReadLine();
                                string[] dp2 = destinationPt2.Split(' ');//分解输入字符串
                                double x2 = double.Parse(dp2[0]);//取得x
                                double y2 = double.Parse(dp2[1]);//取得y
                                CPoint p2 = new CPoint(x2, y2);//产生新的点对象
                                lines[numLn].Moveto(p2, selectedIndex - 1);
                                break;
                            default:
                                Console.WriteLine("ERROR!");
                                break;
                        }
                        break;
                    case "5":
                        Console.Write("Please choose the line's number: ");
                        int addLn = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Which location do you want to insert: ");
                        int addLoc = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Input your destination points");
                        string Pt = Console.ReadLine();
                        string[] add = Pt.Split(' ');//分解输入字符串
                        double addx = double.Parse(add[0]);//取得x
                        double addy = double.Parse(add[1]);//取得y
                        CPoint addPt = new CPoint(addx, addy);//产生新的点对象
                        lines[addLn].AddPoint(addLoc, addPt);
                        break;
                    case "6":
                        //在此处填写代码
                        break;
                    default:
                        bExit = true;
                        break; ;
                }
                Console.WriteLine("-----------------------------");
            } while (!bExit);
        }
    }

    /* 经纬度位置 */
    class CLocation
    {
        private double longitude; //经度
        private double latitude; //纬度
        public double LONGITUDE //属性
        {
            get { return longitude; }
            set { longitude = value; }
        }
        public double LATITUDE
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public CLocation() //构造函数
        {
            longitude = 0;
            latitude = 0;
        }
        public CLocation(double LONG, double LAT)
        {
            longitude = LONG;
            latitude = LAT;
        }
    }

    /* 表达图形概念，包含坐标系统信息 */
    class CShape //图形类
    {
        public long ID; //编号
        protected string CoordinationSystem; //坐标系统
        public const double Tolerance = 0.0001; //定义一个容差，当点之间的距离小于该数值时，被当做是同一个点
        //方法ShowInfo显示对象相关信息
        public void ShowInfo()
        {
            Console.WriteLine("CoordinationSystem is:" + CoordinationSystem);
        }
    }

    /* CShape类的子类：CPoint类，为子类添加静态方法来计算两点之间的距离；添加一个方法用来移动点到指定位置；添加一个转换CLocation类对象为CPoint类对象的类型转换重载函数；添加一个比较两点是否一致的方法 */
    class CPoint : CShape //点类
    {
        private double x;
        private double y;

        public double PX //属性
        {
            get { return x; }
            set { x = value; }
        }
        public double PY //属性
        {
            get { return y; }
            set { y = value; }
        }
        //静态方法GetDistanceBetweenTwoPoints计算两点之间的距离
        public static double GetDistanceBetweenTwoPoints(CPoint Pt1, CPoint Pt2)
        {
            double dis = System.Math.Sqrt((Pt1.PX - Pt2.PX) * (Pt1.PX - Pt2.PX) + (Pt1.PY - Pt2.PY) * (Pt1.PY - Pt2.PY));
            return dis;
        }
        public CPoint() //构造函数
        {
            x = 0;
            y = 0;
        }
        public CPoint(double pX, double pY) //构造函数
        {
            x = pX;
            y = pY;
        }
        //移动点到指定位置
        public void Moveto(CPoint destinationPos)
        {
            x = destinationPos.x;
            y = destinationPos.y;
        }
        //转换位置类对象为点类对象
        public static implicit operator CPoint(CLocation loc)
        {
            //此处忽略经纬度转换为平面坐标系的具体算法
            CPoint pt = new CPoint(loc.LONGITUDE, loc.LATITUDE);
            return pt;
        }
        //判断两个点是否相等
        public bool IsIdentical(CPoint buddyPoint)
        {
            double dis = GetDistanceBetweenTwoPoints(this, buddyPoint);
            if (dis < Tolerance)
                return true;
            else
                return false;
        }
    }

    class CLine : CPoint
    {
        private CPoint[] _Points; //组成线的点数组
        public bool isValid; //是否为有效的线，即点之间的距离是否大于容限

        public CPoint[] POINTS //属性
        {
            get { return _Points; }
            set { _Points = value; }
        }
        public CLine()
        {
            _Points = null;
            isValid = false;
        }
        public CLine(CPoint PT1, CPoint PT2) : base(PT1.PX, PT1.PY) //构造函数
        {
            _Points = new CPoint[2];
            _Points[0] = PT1;
            _Points[1] = PT2;
            SetValidation();
        }
        public CLine(double X1, double Y1, double X2, double Y2) //构造函数
        {
            _Points = new CPoint[2];
            _Points[0] = new CPoint(X1, Y1);
            _Points[1] = new CPoint(X2, Y2);
            //SetValidation();
        }
        public CLine(params CPoint[] PTS) : base(PTS[0].PX, PTS[0].PY)
        {
            _Points = PTS;
            isValid = true; //此处还需要增加条件判断
        }
        //函数GetLineLength计算线的长度
        public double GetLineLength()
        {
            if (_Points.Length < 2)
                return 0;
            double totalDistance = 0;
            int n = _Points.GetLength(0); //取得点的数目 
            for (int i = 1; i < n; i++)
            {
                CPoint p1 = _Points[i - 1];
                CPoint p2 = _Points[i];
                totalDistance += CPoint.GetDistanceBetweenTwoPoints(p1, p2); //注意计算两点之间的距离，累加
            }
            return totalDistance;
        }
        public void SetValidation()
        {
            if (GetLineLength() > Tolerance)
                isValid = true;
            else
                isValid = false;
        }
        //改写父类CPoint中的移动到指定位置的方法，该方法需要考虑整体移动线
        //改写Moveto方法
        new public void Moveto(CPoint destinationPos)
        {
            //按照中心移动，将线段整体移动到目标位置
            int n = _Points.GetLength(0);
            if (n % 2 == 1)
            {
                double distanceX = destinationPos.PX - _Points[n / 2].PX;
                double distanceY = destinationPos.PY - _Points[n / 2].PY;
                foreach (CPoint pt in _Points)
                {
                    pt.PX += distanceX;
                    pt.PY += distanceY;
                }
            }
            else
            {
                double distanceX = destinationPos.PX - (_Points[(n / 2) + 1].PX - _Points[n / 2].PX) / 2;
                double distanceY = destinationPos.PY - (_Points[(n / 2) + 1].PY - _Points[n / 2].PY) / 2;
                foreach (CPoint pt in _Points)
                {
                    pt.PX += distanceX;
                    pt.PY += distanceY;
                }
            }
        }
        //重载Moveto方法，考虑只移动指定顶点到目标位置
        public void Moveto(CPoint destinationPos, int ptIndex)
        {
            double distanceX = destinationPos.PX - _Points[ptIndex].PX;
            double distanceY = destinationPos.PY - _Points[ptIndex].PY;
            foreach (CPoint pt in _Points)
            {
                pt.PX += distanceX;
                pt.PY += distanceY;
            }
        }
        //增加比较大小的逻辑运算符">"、"<"的重载方法
        //重载操作符">"，比较二者长度
        public static bool operator >(CLine ln1, CLine ln2)
        {
            double length1 = ln1.GetLineLength();
            double length2 = ln2.GetLineLength();
            if (length1 > length2)
                return true;
            else
                return false;
        }
        public static bool operator <(CLine ln1, CLine ln2)
        {
            double length1 = ln1.GetLineLength();
            double length2 = ln2.GetLineLength();
            if (length1 < length2)
                return true;
            else
                return false;
        }
        //重载加法"+"操作符，可以把两个线连接在一起成为一个新的线要素
        //重载操作符"+",把二个在首或尾有关联的线连接成一个新的线段
        public static CLine operator +(CLine ln1, CLine ln2)
        {
            CLine tempLine = null; //新的线段
            int m = ln1.POINTS.GetLength(0); //第一条线的点数
            int n = ln2.POINTS.GetLength(0); //第二条线的点数
            CPoint firstStart = ln1.POINTS[0]; //第一条线的起点
            CPoint firstEnd = ln1.POINTS[m - 1]; //第一条线的终点
            CPoint secondStart = ln2.POINTS[0]; //第二条线的起点
            CPoint secondEnd = ln2.POINTS[m - 1]; //第二条线的终点
            double ss = CPoint.GetDistanceBetweenTwoPoints(firstStart, secondStart);//第一条线起点与第二条线起点之间的距离
            double se = CPoint.GetDistanceBetweenTwoPoints(firstStart, secondEnd);//第一条线起点与第二条线终点之间的距离
            double es = CPoint.GetDistanceBetweenTwoPoints(firstEnd, secondStart);//第一条线终点与第二条线起点之间的距离
            double ee = CPoint.GetDistanceBetweenTwoPoints(firstEnd, secondEnd);//第一条线终点与第二条线终点之间的距离
            if (ss < Tolerance)//在容限范围内
            {
                CPoint[] pts = new CPoint[m + n - 1];
                for (int i = m - 1; i >= 0; i--)
                    pts[m - i - 1] = ln1.POINTS[i];//逆序拷贝第一个线要素的点
                Array.Copy(ln2.POINTS, 0, pts, m, n);//顺序拷贝第二个线要素的点
                tempLine = new CLine(pts);
            }
            else if (se < Tolerance)
            {
                CPoint[] pts = new CPoint[m + n - 1];
                for (int i = m - 1; i >= 0; i--)
                    pts[m - i - 1] = ln1.POINTS[i]; //逆序拷贝第一个线要素的点
                for (int j = n - 1; j >= 0; j--)
                    pts[m + n - j - 1] = ln2.POINTS[j]; //逆序拷贝第二个线要素的点
                tempLine = new CLine(pts);
            }
            else if (es < Tolerance)
            {
                CPoint[] pts = new CPoint[m + n - 1];
                Array.Copy(ln1.POINTS, 0, pts, 0, m); //顺序拷贝第一个线要素的点
                Array.Copy(ln2.POINTS, 0, pts, m, n); //顺序拷贝第二个线要素的点
                tempLine = new CLine(pts);
            }
            else if (ee < Tolerance)
            {
                CPoint[] pts = new CPoint[m + n - 1];
                Array.Copy(ln1.POINTS, pts, m); //顺序拷贝第一个线要素的点
                for (int i = n - 1; i >= 0; i--)
                    pts[m + n - i - 1] = ln2.POINTS[i]; //逆序拷贝第二个线要素的点
                tempLine = new CLine(pts);
            }
            return tempLine;
        }
        //添加方法，在指定点索引位置为线要素增加新的点
        //方法AddPoint在指定位置处插入点
        public bool AddPoint(int insertIndex, CPoint insertPt)
        {
            bool success = false;
            CPoint OriginalPt = _Points[insertIndex - 1]; //插入位置原来的点
            CPoint FormerPt = _Points[insertIndex - 2]; //插入位置前一个的点
            if (OriginalPt.IsIdentical(insertPt) && FormerPt.IsIdentical(insertPt))
            {

                int length = _Points.GetLength(0);
                CPoint[] pts = new CPoint[_Points.GetLength(0) + 1];
                for (int i = _Points.GetLength(0) + 1; i > insertIndex; i--)
                {
                    pts[i] = _Points[i - 1];
                }
                pts[insertIndex] = insertPt;
                for (int j = 0; j < insertIndex; j++)
                {
                    pts[j] = _Points[j];
                }
                _Points = pts;
                success = true;
            }
            //double ss = OriginalPt.PX - insertPt.PX; //插入点与原点x轴距离
            //double se = OriginalPt.PY- insertPt.PY;//插入点与原点y轴距离
            //double es = ;//插入点与原点前一个点y轴距离
            //double ee = ;//插入点与原点前一个点y轴距离
            return success;
        }
        //添加方法移除指定位置的点，并获得新的线对象
        //方法RemovePointAt移除指定位置的点，并得到新的线对象
        //public bool RemovePointAt(int pos,out CLine newLine) //??out
        //{
        //    bool success = false;
        //此处添加代码
        //return success;
        //}
    }
}
