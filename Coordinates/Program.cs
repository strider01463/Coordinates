// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

//namespace Coordinates
//{
    public class Program
    {

        public static void Main(string[] args)
        {
            Int32 Xcoordarg = 0;
            Int32 Ycoordarg = 0;

            if (args.Length == 0)
            {
                Xcoordarg = 10;
                Ycoordarg = 10;

                Console.Write("No x and y coordinates present. Defaulting to 1,1 for starting position.");
            }
            else
            {
                Xcoordarg = Convert.ToInt32(args[0].ToString());
                Ycoordarg = Convert.ToInt32(args[1].ToString());
            }

            List<Item> coordinates = new List<Item>();
            List<SortedItem> sortedPoints = new List<SortedItem>();
            Console.Title = "Closest Coordinates Program";

            coordinates = LoadCoordinates();
            sortedPoints = ProcessCoordinates(coordinates, Xcoordarg, Ycoordarg);
            PrintCoordinates(sortedPoints);
        }

        public static List<Item> LoadCoordinates()
        {
            using (StreamReader JsonReader = new StreamReader(@"C:\Users\strid\OneDrive\Documents\Assessments\Coordinates.json"))
            {
                string json = JsonReader.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);

                return items;
            }
        }

        public static List<SortedItem> ProcessCoordinates(List<Item> xycoordinates, Int32 startx, Int32 starty)
        {
            // process the list of coordinates.
            var newcoordinates = new List<SortedItem>();
            foreach (var coord in xycoordinates)
            {
                var str = coord.Value.Split(new char[] { ',' });
                newcoordinates.Add(new SortedItem() { pointID = coord.ID.ToString(), xCoordinate = Int32.Parse(str[0]), yCoordinate = Int32.Parse(str[1]), distance = CalculateDistance(startx, starty, Int32.Parse(str[0]), Int32.Parse(str[1])) });

            }

            return newcoordinates;
        }

        public static void PrintCoordinates(List<SortedItem> closestpoints)
          {
            //sort the order of coordinates closest to farthest.
            var sortedClosestPointsList = new List<SortedItem>();
            sortedClosestPointsList = closestpoints.OrderBy(p => (p.distance))
                                 .ToList();

        Console.WriteLine("{0} {1} {2} {3}", "ID   ", "X Coordinate", "Y Coordinate", "Distance from Start");

        foreach (var item in sortedClosestPointsList)
            {
                Console.WriteLine("{0} {1} {2} {3}", item.pointID, item.xCoordinate, item.yCoordinate, item.distance);
            }
        }

        private static double CalculateDistance(Int32 x1, Int32 y1, Int32 x2, Int32 y2)
        {
            // Calculate the distance between the starting point and the current x,y coordinates passed.
            double distance = 0;
            distance = Math.Abs(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));

            return distance;
        }

        public class Item
        {
            public string ID { get; set; }
            public string Value { get; set; }
        }

        public class SortedItem
        {
            public string pointID { get; set; }
            public Int32 xCoordinate { get; set; }
            public Int32 yCoordinate { get; set; }
            public double distance { get; set; }

        }

        public class Coordinates
        {
            public Int32 xpoint { get; set; }
            public Int32 ypoint { get; set; }

        }
    }
//}