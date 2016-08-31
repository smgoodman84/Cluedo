using System.Collections.Generic;
using System.Linq;

namespace Cluedo
{
    internal class Board
    {
        private static readonly BoardPosition Void = new BoardPosition("Void");
        private static readonly BoardPosition VerticalDoor = new BoardPosition("VerticalDoor");
        private static readonly BoardPosition HorizontalDoor = new BoardPosition("HorizontalDoor");

        public static readonly BoardPosition ReverendGreenStart = new BoardPosition("ReverendGreenStart");
        public static readonly BoardPosition MrsPeacockStart = new BoardPosition("MrsPeacockStart");
        public static readonly BoardPosition ProfessorPlumStart = new BoardPosition("ProfessorPlumStart");
        public static readonly BoardPosition MissScarletStart = new BoardPosition("MissScarletStart");
        public static readonly BoardPosition ColonelMustardStart = new BoardPosition("ColonelMustardStart");
        public static readonly BoardPosition MrsWhiteStart = new BoardPosition("MrsWhiteStart");

        public static readonly BoardPosition KitchenStudyPassage = new BoardPosition("KitchenStudyPassage");
        public static readonly BoardPosition LoungeConservatoryPassage = new BoardPosition("LoungeConservatoryPassage");


        private static readonly List<BoardPosition> RoomsAndPassages = new List<BoardPosition>
        {
            Room.Kitchen,
            Room.BallRoom,
            Room.Conservatory,
            Room.DiningRoom,
            Room.BilliardRoom,
            Room.Library,
            Room.Lounge,
            Room.Hall,
            Room.Study,
            KitchenStudyPassage,
            LoungeConservatoryPassage
        };

        private const string Layout = @"
         W    G
KKKK|T ...AAAA... CCCCCC
KKKKKK..AAAAAAAA..CCCCCC
KKKKKK..AAAAAAAA..CCCCCC
KKKKKK..AAAAAAAA..-CCCCC
KKKKKK..|AAAAAA|...CC|U
 KKK-K..AAAAAAAA.......E
........A-AAAA-A.......
 .................BBBBBB
DDDDD.............|BBBBB
DDDDDDDD..     ...BBBBBB
DDDDDDDD..     ...BBBBBB
DDDDDDD|..     ...BBBB-B
DDDDDDDD..     ........
DDDDDDDD..     ...LL-LL
DDDDDD-D..     ..LLLLLLL
 .........     ..|LLLLLL
M................LLLLLLL
 ........HH--HH...LLLLL
U|OOOO-..HHHHHH........P
OOOOOOO..HHHHH|........
OOOOOOO..HHHHHH..-SSSS|T
OOOOOOO..HHHHHH..SSSSSSS
OOOOOOO..HHHHHH..SSSSSSS
OOOOOO R HHHHHH . SSSSSS
";

        private static BoardPosition MapPosition(char layoutCharacter)
        {
            switch (layoutCharacter)
            {
                case 'A': return Room.BallRoom;
                case 'B': return Room.BilliardRoom;
                case 'C': return Room.Conservatory;
                case 'D': return Room.DiningRoom;
                case 'E': return MrsPeacockStart;
                case 'G': return ReverendGreenStart;
                case 'H': return Room.Hall;
                case 'K': return Room.Kitchen;
                case 'L': return Room.Library;
                case 'M': return ColonelMustardStart;
                case 'O': return Room.Lounge;
                case 'P': return ProfessorPlumStart;
                case 'R': return MissScarletStart;
                case 'S': return Room.Study;
                case 'T': return KitchenStudyPassage;
                case 'U': return LoungeConservatoryPassage;
                case 'W': return MrsWhiteStart;
                case '.': return new BoardPosition();
                case '|': return VerticalDoor;
                case '-': return HorizontalDoor;
                default: return Void;
            }
        }

        public Board()
        {
            var boardPositionArray = BoardPositionArray();

            var height = boardPositionArray.Length;
            var width = boardPositionArray[0].Length;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var boardPosition = boardPositionArray[y][x];

                    if (boardPosition == HorizontalDoor)
                    {
                        boardPositionArray[y - 1][x].Neighbours.Add(boardPositionArray[y + 1][x]);
                        boardPositionArray[y + 1][x].Neighbours.Add(boardPositionArray[y - 1][x]);
                        continue;
                    }

                    if (boardPosition == VerticalDoor)
                    {
                        boardPositionArray[y][x - 1].Neighbours.Add(boardPositionArray[y][x + 1]);
                        boardPositionArray[y][x + 1].Neighbours.Add(boardPositionArray[y][x - 1]);
                        continue;
                    }

                    if (RoomsAndPassages.Contains(boardPosition))
                    {
                        continue;
                    }

                    var neighbours = new List<BoardPosition>();
                    if (x > 0)
                    {
                        neighbours.Add(boardPositionArray[y][x - 1]);
                    }
                    if (x < width - 1)
                    {
                        neighbours.Add(boardPositionArray[y][x + 1]);
                    }
                    if (y > 0)
                    {
                        neighbours.Add(boardPositionArray[y - 1][x]);
                    }
                    if (y < height - 1)
                    {
                        neighbours.Add(boardPositionArray[y + 1][x]);
                    }

                    boardPosition.Neighbours.AddRange(
                        neighbours
                        .Where(n => n != Void)
                        .Where(n => n != boardPosition)
                        .Where(n => !RoomsAndPassages.Contains(n))
                        .Where(n => !boardPosition.Neighbours.Contains(n))
                        .Distinct());

                    if (boardPosition.Name == null)
                    {
                        boardPosition.Name = $"Passageway[{x},{y}]";
                    }
                }
            }
        }

        private static BoardPosition[] MapLine(IEnumerable<char> line)
        {
            return line.Select(MapPosition).ToArray();
        }

        private static BoardPosition[][] BoardPositionArray()
        {
            var layoutLines = Layout
                .Replace("\r", "")
                .Split('\n')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            var width = layoutLines.Max(x => x.Length);

            var result = layoutLines
                .Select(x => x.PadRight(width, ' ').ToCharArray())
                .Select(MapLine)
                .ToArray();

            return result;
        }
    }
}
