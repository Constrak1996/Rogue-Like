using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rogue_Like
{
    class NLR_Level2 : State
    {

        private SpriteFont Font;
        private Texture2D _playerTexture;




        //Tilemap of Dungeon Background Map
        private int[,] mapBackground = new int[,]
       {
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,104,105,106,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
            {-1,-1,95,96,96,96,96,96,96,96,96,97,14,14,14,95,96,96,96,96,96,96,96,96,97,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,14,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,14,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,100,99,99,99,99,99,99,99,99,98,14,14,14,100,99,99,99,99,99,99,99,99,98,-1,-1},
            {-1,-1,101,102,102,102,102,102,102,102,102,103,14,14,14,101,102,102,102,102,102,102,102,102,103,-1,-1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,14,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1},

       };
        public int GetIndexBackGround(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return mapBackground[cellY, cellX];
        }
        //Tilemap of Dungeon Map
        public static int[,] map = new int[,]
       {
            {4,41,42,42,42,42,42,39,42,42,42,42,46,47,48,38,38,38,38,39,38,38,38,38,38,37,33},
            {6,5,43,43,43,43,43,40,43,43,76,43,49,50,51,44,76,44,44,40,44,44,44,44,44,34,32},
            {7,16,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,70,-1,70,-1,-1,-1,-1,-1,-1,-1,-1,-1,70,36,31},
            {7,79,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,77,31},
            {7,16,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,31},
            {8,9,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,30,29},
            {7,16,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,31},
            {7,16,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,31},
            {10,16,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,28},
            {10,15,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,28},
            {10,15,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,28},
            {8,9,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,30,29},
            {10,15,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,36,28},
            {10,79,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,77,28},
            {10,15,70,-1,-1,-1,-1,-1,-1,-1,-1,-1,70,-1,70,-1,-1,-1,-1,-1,-1,-1,-1,-1,70,36,28},
            {11,13,25,25,25,25,25,20,25,25,78,25,63,62,61,25,78,25,25,20,25,25,25,25,25,24,27},
            {12,17,18,18,18,18,18,19,18,18,18,18,60,59,58,21,21,21,21,19,21,21,21,21,21,22,23},

       };

        public int GetIndex(int cellX, int cellY)
        {
            if (cellX < 0 || cellX > Width - 1 || cellY < 0 || cellY > Height - 1)
                return 0;

            return map[cellY, cellX];
        }

        public static List<Texture2D> tileTextures = new List<Texture2D>();
        //add Textures to the Dungeon map
        public void AddTexture(Texture2D texture)
        {
            tileTextures.Add(texture);
        }

        #region Numeral Texture
        //The Width of Dungeon map
        public int Width
        {
            get { return map.GetLength(1); }
        }
        //Height of Dungeon map
        public int Height
        {
            get { return map.GetLength(0); }
        }

        public int Ground
        {
            get { return map.GetLength(2); }
        }

        public int DoorFront
        {
            get { return map.GetLength(3); }
        }

        public int WallCornerTopLeft_1
        {
            get { return map.GetLength(4); }
        }

        public int WallCornerTopLeft_2
        {
            get { return map.GetLength(5); }
        }

        public int WallTopLeft_1
        {
            get { return map.GetLength(6); }
        }

        public int WallTopLeft_2
        {
            get { return map.GetLength(7); }
        }

        public int WallMidLeft_1
        {
            get { return map.GetLength(8); }
        }

        public int WallMidLeft_2
        {
            get { return map.GetLength(9); }
        }

        public int WallBotLeft_2
        {
            get { return map.GetLength(10); }
        }

        public int WallBotLeft_1
        {
            get { return map.GetLength(11); }
        }

        public int WallCornerBotLeft_1
        {
            get { return map.GetLength(12); }
        }

        public int WallCornerBotLeft_2
        {
            get { return map.GetLength(13); }
        }

        public int Floor_Pattern_1
        {
            get { return map.GetLength(14); }
        }

        public int WallLowerPartBot
        {
            get { return map.GetLength(15); }
        }

        public int WallLowerPartTop
        {
            get { return map.GetLength(16); }
        }

        public int WallLeftBottom_1
        {
            get { return map.GetLength(17); }
        }

        public int WallLeftBottom_2
        {
            get { return map.GetLength(18); }
        }

        public int WallMiddleBottom_1
        {
            get { return map.GetLength(19); }
        }

        public int WallMiddleBottom_2
        {
            get { return map.GetLength(20); }
        }

        public int WallRightBottom_2
        {
            get { return map.GetLength(21); }
        }

        public int WallRightBottom_1
        {
            get { return map.GetLength(22); }
        }

        public int WallCornerRightBottom_1
        {
            get { return map.GetLength(23); }
        }

        public int WallCornerRightBottom_2
        {
            get { return map.GetLength(24); }
        }

        public int WallLeftBottomLow_1
        {
            get { return map.GetLength(25); }
        }

        public int WallLeftBottomLow_2
        {
            get { return map.GetLength(26); }
        }

        public int WallRightLeft_1
        {
            get { return map.GetLength(27); }
        }

        public int WallRightLeft_2
        {
            get { return map.GetLength(28); }
        }

        public int WallRightMiddle_1
        {
            get { return map.GetLength(29); }
        }

        public int WallRightMiddle_2
        {
            get { return map.GetLength(30); }
        }

        public int WallRightTop_2
        {
            get { return map.GetLength(31); }
        }

        public int WallRightTop_1
        {
            get { return map.GetLength(32); }
        }

        public int WallCornerTopRight_1
        {
            get { return map.GetLength(33); }
        }

        public int WallCornerTopRight_2
        {
            get { return map.GetLength(34); }
        }

        public int wallLeftRightLow
        {
            get { return map.GetLength(35); }
        }

        public int wallRightRightLow
        {
            get { return map.GetLength(36); }
        }

        public int wallTopRight1
        {
            get { return map.GetLength(37); }
        }

        public int wallTopRight2
        {
            get { return map.GetLength(38); }
        }

        public int wallTopMid1
        {
            get { return map.GetLength(39); }
        }

        public int wallTopMid2
        {
            get { return map.GetLength(40); }
        }

        public int wallTopLeft1
        {
            get { return map.GetLength(41); }
        }

        public int wallTopLeft2
        {
            get { return map.GetLength(42); }
        }

        public int wallLeftTopLow
        {
            get { return map.GetLength(43); }
        }

        public int wallRightTopLow
        {
            get { return map.GetLength(44); }
        }

        public int Floor_Pattern_2
        {
            get { return map.GetLength(45); }
        }

        public int Door_Left_Top_Top
        {
            get { return map.GetLength(46); }
        }

        public int Door_Mid_Top_Top
        {
            get { return map.GetLength(47); }
        }

        public int Door_Right_Top_Top
        {
            get { return map.GetLength(48); }
        }

        public int Door_Left_Bot_Top
        {
            get { return map.GetLength(49); }
        }

        public int Door_Mid_Bot_Top
        {
            get { return map.GetLength(50); }
        }

        public int Door_Right_Bot_Top
        {
            get { return map.GetLength(51); }
        }

        public int Door_Left_Top_Right
        {
            get { return map.GetLength(52); }
        }

        public int Door_Mid_Top_Right
        {
            get { return map.GetLength(53); }
        }

        public int Door_Right_Top_Right
        {
            get { return map.GetLength(54); }
        }

        public int Door_Left_Bot_Right
        {
            get { return map.GetLength(55); }
        }

        public int Door_Mid_Bot_Right
        {
            get { return map.GetLength(56); }
        }

        public int Door_Right_Bot_Right
        {
            get { return map.GetLength(57); }
        }

        public int Door_Left_Top_Bottom
        {
            get { return map.GetLength(58); }
        }

        public int Door_Mid_Top_Bottom
        {
            get { return map.GetLength(59); }
        }

        public int Door_Right_Top_Bottom
        {
            get { return map.GetLength(60); }
        }

        public int Door_Left_Bot_Bottom
        {
            get { return map.GetLength(61); }
        }

        public int Door_Mid_Bot_Bottom
        {
            get { return map.GetLength(62); }
        }

        public int Door_Right_Bot_Bottom
        {
            get { return map.GetLength(63); }
        }

        public int Door_Left_Top_Left
        {
            get { return map.GetLength(64); }
        }

        public int Door_Mid_Top_Left
        {
            get { return map.GetLength(65); }
        }

        public int Door_Right_Top_Left
        {
            get { return map.GetLength(66); }
        }

        public int Door_Left_Bot_Left
        {
            get { return map.GetLength(67); }
        }

        public int Door_Mid_Bot_Left
        {
            get { return map.GetLength(68); }
        }

        public int Door_Right_Bot_Left
        {
            get { return map.GetLength(69); }
        }

        public int Brazier
        {
            get { return map.GetLength(70); }
        }

        public int Rocks
        {
            get { return map.GetLength(71); }
        }

        public int Wall_Shield_Top
        {
            get { return map.GetLength(72); }
        }

        public int Wall_Shield_Right
        {
            get { return map.GetLength(73); }
        }

        public int Wall_Shield_Bot
        {
            get { return map.GetLength(74); }
        }

        public int Wall_Shield_Left
        {
            get { return map.GetLength(75); }
        }

        public int Wall_SwordShield_Top
        {
            get { return map.GetLength(76); }
        }

        public int Wall_SwordShield_Right
        {
            get { return map.GetLength(77); }
        }

        public int Wall_SwordShield_Bot
        {
            get { return map.GetLength(78); }
        }

        public int Wall_SwordShield_Left
        {
            get { return map.GetLength(79); }
        }

        public int Carpet_Top_Left_Corner
        {
            get { return map.GetLength(80); }
        }

        public int Carpet_Top_Mid
        {
            get { return map.GetLength(81); }
        }

        public int Carpet_Top_Right_Corner
        {
            get { return map.GetLength(82); }
        }

        public int Carpet_Left_Mid
        {
            get { return map.GetLength(83); }
        }

        public int Carpet_Left_Bot_Corner
        {
            get { return map.GetLength(84); }
        }

        public int Carpet_Bot_Mid
        {
            get { return map.GetLength(85); }
        }

        public int Carpet_Bot_Right_Corner
        {
            get { return map.GetLength(86); }
        }

        public int Carpet_Right_Mid
        {
            get { return map.GetLength(87); }
        }

        public int Carpet_Mid_Mid
        {
            get { return map.GetLength(88); }
        }

        public int Door_Left_Top_Entry
        {
            get { return map.GetLength(89); }
        }

        public int Door_Mid_Top_Entry
        {
            get { return map.GetLength(90); }
        }

        public int Door_Right_Top_Entry
        {
            get { return map.GetLength(91); }
        }

        public int Door_Left_Bot_Entry
        {
            get { return map.GetLength(92); }
        }

        public int Door_Mid_Bot_Entry
        {
            get { return map.GetLength(93); }
        }

        public int Door_Right_Bot_Entry
        {
            get { return map.GetLength(94); }
        }

        public int Hole_Top_Left
        {
            get { return map.GetLength(95); }
        }

        public int Hole_Top_Mid
        {
            get { return map.GetLength(96); }
        }

        public int Hole_Top_Right
        {
            get { return map.GetLength(97); }
        }

        public int Hole_Right_Mid
        {
            get { return map.GetLength(98); }
        }

        public int Hole_Mid
        {
            get { return map.GetLength(99); }
        }

        public int Hole_Left_Mid
        {
            get { return map.GetLength(100); }
        }

        public int Hole_Bottom_Left
        {
            get { return map.GetLength(101); }
        }

        public int Hole_Bottom_Mid
        {
            get { return map.GetLength(102); }
        }

        public int Hole_Bottom_Right
        {
            get { return map.GetLength(103); }
        }

        public int Stair_Top_Left
        {
            get { return map.GetLength(104); }
        }

        public int Stair_Top_Mid
        {
            get { return map.GetLength(105); }
        }

        public int Stair_Top_Right
        {
            get { return map.GetLength(106); }
        }

        public int Stair_Bot_Left
        {
            get { return map.GetLength(107); }
        }

        public int Stair_Bot_Mid
        {
            get { return map.GetLength(108); }
        }

        public int Stair_Bot_Right
        {
            get { return map.GetLength(109); }
        }
        #endregion




        public NLR_Level2(GameWorld gameWorld, GraphicsDevice graphicsDevice, ContentManager content) : base(gameWorld, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");
            Font = content.Load<SpriteFont>("Font");
            Texture2D piller = content.Load<Texture2D>("Pillar1");
            #region Texture loaded
            //Left Wall
            Texture2D wallTopCorLeft = content.Load<Texture2D>("64x64/Wall_Corner_Top_Left");
            Texture2D wallTopCorLeft2 = content.Load<Texture2D>("64x64/Wall_Corner_Top_Left_2");
            Texture2D wallTopLeft = content.Load<Texture2D>("64x64/Wall_Left_Up_1");
            Texture2D wallTopLeft2 = content.Load<Texture2D>("64x64/Wall_Left_Up_2");
            Texture2D wallMidLeftTop = content.Load<Texture2D>("64x64/Wall_Mid_Left_Top");
            Texture2D wallMidLeftLow = content.Load<Texture2D>("64x64/Wall_Mid_Left_Low");
            Texture2D wallBotLeft2 = content.Load<Texture2D>("64x64/Wall_Left_Bottom_2");
            Texture2D wallBotLeft1 = content.Load<Texture2D>("64x64/Wall_Left_Bottom_1");
            Texture2D wallBotCorLeft1 = content.Load<Texture2D>("64x64/Wall_Corner_Bot_Left_1");
            Texture2D wallBotCorLeft2 = content.Load<Texture2D>("64x64/Wall_Corner_Bot_Left_2");
            Texture2D wallBotLow = content.Load<Texture2D>("64x64/Wall_Left_Left_Low_2");
            Texture2D wallTopLow = content.Load<Texture2D>("64x64/Wall_Right_Left_Low_1");
            //Bottom Wall
            Texture2D wallBottomLeft1 = content.Load<Texture2D>("64x64/Wall_Left_Bottom_Up_1");
            Texture2D wallBottomLeft2 = content.Load<Texture2D>("64x64/Wall_Left_Bot_2");
            Texture2D wallBottomMiddle1 = content.Load<Texture2D>("64x64/Wall_Mid_Bottom_Up");
            Texture2D wallBottomMiddle2 = content.Load<Texture2D>("64x64/Wall_Mid_Bottom_Low");
            Texture2D wallBottomRight2 = content.Load<Texture2D>("64x64/Wall_Right_Bottom_2");
            Texture2D wallBottomRight1 = content.Load<Texture2D>("64x64/Wall_Right_Bottom_1");
            Texture2D wallCornerBottomRight1 = content.Load<Texture2D>("64x64/Wall_Corner_Bot_Right_1");
            Texture2D wallCornerBottomRight2 = content.Load<Texture2D>("64x64/Wall_Corner_Bot_Right_2");
            Texture2D wallLeftBotLow = content.Load<Texture2D>("64x64/Wall_Left_Bottom_Low_2");
            Texture2D wallRightBotLow = content.Load<Texture2D>("64x64/Wall_Right_Bottom_Low_1");
            //Right Wall
            Texture2D wallRightLeft1 = content.Load<Texture2D>("64x64/Wall_Right_Bottom_Up_1");
            Texture2D wallRightLeft2 = content.Load<Texture2D>("64x64/Wall_Right_Bottom_Up_2");
            Texture2D wallRightMiddle1 = content.Load<Texture2D>("64x64/Wall_Mid_Right_Up");
            Texture2D wallRightMiddle2 = content.Load<Texture2D>("64x64/Wall_Mid_Right_Low");
            Texture2D wallRightTop2 = content.Load<Texture2D>("64x64/Wall_Right_Top_2");
            Texture2D wallRightTop1 = content.Load<Texture2D>("64x64/Wall_Right_Top_1");
            Texture2D wallCornerTopRight1 = content.Load<Texture2D>("64x64/Wall_Corner_Top_Right_1");
            Texture2D wallCornerTopRight2 = content.Load<Texture2D>("64x64/Wall_Corner_Top_Right_2");
            Texture2D wallLeftRightLow = content.Load<Texture2D>("64x64/Wall_Right_Right_Low_1");
            Texture2D wallRightRightLow = content.Load<Texture2D>("64x64/Wall_Left_Right_Low_2");
            //Top Wall
            Texture2D wallTopRight1 = content.Load<Texture2D>("64x64/Wall_Top_Right_1");
            Texture2D wallTopRight2 = content.Load<Texture2D>("64x64/Wall_Top_Right_2");
            Texture2D wallTopMiddle1 = content.Load<Texture2D>("64x64/Wall_Mid_Top_Up");
            Texture2D wallTopMiddle2 = content.Load<Texture2D>("64x64/Wall_Mid_Top_Low");
            Texture2D wallTopLeft_2 = content.Load<Texture2D>("64x64/Wall_Top_Left_1");
            Texture2D wallTopLeft_1 = content.Load<Texture2D>("64x64/Wall_Top_Left_2");
            Texture2D wallLeftTopLow = content.Load<Texture2D>("64x64/Wall_Right_Top_Low_2");
            Texture2D wallRightTopLow = content.Load<Texture2D>("64x64/Wall_Right_Top_Low_1");
            //Floor
            Texture2D floorPattern1 = content.Load<Texture2D>("64x64/Floor_1");
            Texture2D floorPattern2 = content.Load<Texture2D>("64x64/Floor_2");
            Texture2D floorPlain = content.Load<Texture2D>("64x64/Plain_Floor");
            Texture2D floorBrick = content.Load<Texture2D>("64x64/Brick_Floor");
            Texture2D floorPlainGray = content.Load<Texture2D>("64x64/Plain_Floor_Gray");
            Texture2D floorRockyGray = content.Load<Texture2D>("64x64/Rocky_Floor_Gray");
            //Door
            //Door Entry
            Texture2D Door_Left_Top_Entry = content.Load<Texture2D>("64x64/Door_Left_Top_Entry");
            Texture2D Door_Mid_Top_Entry = content.Load<Texture2D>("64x64/Door_Mid_Top_Entry");
            Texture2D Door_Right_Top_Entry = content.Load<Texture2D>("64x64/Door_Right_Top_Entry");
            Texture2D Door_Left_Bot_Entry = content.Load<Texture2D>("64x64/Door_Left_Bot_Entry");
            Texture2D Door_Mid_Bot_Entry = content.Load<Texture2D>("64x64/Door_Mid_Bot_Entry");
            Texture2D Door_Right_Bot_Entry = content.Load<Texture2D>("64x64/Door_Right_Bot_Entry");
            //Top Door
            Texture2D Door_Left_Top_Top = content.Load<Texture2D>("64x64/Door_Left_Top_Top");
            Texture2D Door_Mid_Top_Top = content.Load<Texture2D>("64x64/Door_Mid_Top_Top");
            Texture2D Door_Right_Top_Top = content.Load<Texture2D>("64x64/Door_Right_Top_Top");
            Texture2D Door_Left_Bot_Top = content.Load<Texture2D>("64x64/Door_Left_Bot_Top");
            Texture2D Door_Mid_Bot_Top = content.Load<Texture2D>("64x64/Door_Mid_Bot_Top");
            Texture2D Door_Right_Bot_Top = content.Load<Texture2D>("64x64/Door_Right_Bot_Top");
            //Right Door
            Texture2D Door_Left_Top_Right = content.Load<Texture2D>("64x64/Door_Left_Top_Right");
            Texture2D Door_Mid_Top_Right = content.Load<Texture2D>("64x64/Door_Mid_Top_Right");
            Texture2D Door_Right_Top_Right = content.Load<Texture2D>("64x64/Door_Right_Top_Right");
            Texture2D Door_Left_Bot_Right = content.Load<Texture2D>("64x64/Door_Left_Bot_Right");
            Texture2D Door_Mid_Bot_Right = content.Load<Texture2D>("64x64/Door_Mid_Bot_Right");
            Texture2D Door_Right_Bot_Right = content.Load<Texture2D>("64x64/Door_Right_Bot_Right");
            //Bottom Door
            Texture2D Door_Left_Top_Bottom = content.Load<Texture2D>("64x64/Door_Left_Top_Bottom");
            Texture2D Door_Mid_Top_Bottom = content.Load<Texture2D>("64x64/Door_Mid_Top_Bottom");
            Texture2D Door_Right_Top_Bottom = content.Load<Texture2D>("64x64/Door_Right_Top_Bottom");
            Texture2D Door_Left_Bot_Bottom = content.Load<Texture2D>("64x64/Door_Left_Bot_Bottom");
            Texture2D Door_Mid_Bot_Bottom = content.Load<Texture2D>("64x64/Door_Mid_Bot_Bottom");
            Texture2D Door_Right_Bot_Bottom = content.Load<Texture2D>("64x64/Door_Right_Bot_Bottom");
            //Left Door
            Texture2D Door_Left_Top_Left = content.Load<Texture2D>("64x64/Door_Left_Top_Left");
            Texture2D Door_Mid_Top_Left = content.Load<Texture2D>("64x64/Door_Mid_Top_Left");
            Texture2D Door_Right_Top_Left = content.Load<Texture2D>("64x64/Door_Right_Top_Left");
            Texture2D Door_Left_Bot_Left = content.Load<Texture2D>("64x64/Door_Left_Bot_Left");
            Texture2D Door_Mid_Bot_Left = content.Load<Texture2D>("64x64/Door_Mid_Bot_Left");
            Texture2D Door_Right_Bot_Left = content.Load<Texture2D>("64x64/Door_Right_Bot_Left");

            //Carpet
            Texture2D Carpet_Top_Left = content.Load<Texture2D>("64x64/Carpet_Left_Top_Corner");
            Texture2D Carpet_Top_Mid = content.Load<Texture2D>("64x64/Carpet_Top_Mid");
            Texture2D Carpet_Top_Right = content.Load<Texture2D>("64x64/Carpet_Top_Right_Corner");
            Texture2D Carpet_Left_Mid = content.Load<Texture2D>("64x64/Carpet_Left_Mid");
            Texture2D Carpet_Left_Bot = content.Load<Texture2D>("64x64/Carpet_Left_Bot_Corner");
            Texture2D Carpet_Bot_Mid = content.Load<Texture2D>("64x64/Carpet_Bot_Mid");
            Texture2D Carpet_Bot_Right = content.Load<Texture2D>("64x64/Carpet_Bot_Right_Corner");
            Texture2D Carpet_Right_Mid = content.Load<Texture2D>("64x64/Carpet_Right_Mid");
            Texture2D Carpet_Mid = content.Load<Texture2D>("64x64/Carpet_Mid");

            //Hole
            Texture2D Hole_Top_Left = content.Load<Texture2D>("64x64/Hole_Top_Left");
            Texture2D Hole_Top_Mid = content.Load<Texture2D>("64x64/Hole_Top_Mid");
            Texture2D Hole_Top_Right = content.Load<Texture2D>("64x64/Hole_Top_Right");
            Texture2D Hole_Left_Mid = content.Load<Texture2D>("64x64/Hole_Left_Mid");
            Texture2D Hole_Bottom_Left = content.Load<Texture2D>("64x64/Hole_Bottom_Left");
            Texture2D Hole_Bottom_Mid = content.Load<Texture2D>("64x64/Hole_Bottom_Mid");
            Texture2D Hole_Bottom_Right = content.Load<Texture2D>("64x64/Hole_Bottom_Right");
            Texture2D Hole_Right_Mid = content.Load<Texture2D>("64x64/Hole_Right_Mid");
            Texture2D Hole_Mid = content.Load<Texture2D>("64x64/Hole_Mid");

            //Stairs
            Texture2D Stair_Top_Left = content.Load<Texture2D>("64x64/Stair_Top_Left");
            Texture2D Stair_Top_Mid = content.Load<Texture2D>("64x64/Stair_Top_Mid");
            Texture2D Stair_Top_Right = content.Load<Texture2D>("64x64/Stair_Top_Right");
            Texture2D Stair_Bot_Left = content.Load<Texture2D>("64x64/Stair_Bot_Left");
            Texture2D Stair_Bot_Mid = content.Load<Texture2D>("64x64/Stair_Bot_Mid");
            Texture2D Stair_Bot_Right = content.Load<Texture2D>("64x64/Stair_Bot_Right");

            //Misc.
            Texture2D Brazier = content.Load<Texture2D>("64x64/Brazier");
            Texture2D Rocks = content.Load<Texture2D>("64x64/Rocks");
            Texture2D ShieldTop = content.Load<Texture2D>("64x64/Wall_Right_Top_Low_Shield");
            Texture2D ShieldRight = content.Load<Texture2D>("64x64/Wall_Right_Right_Low_Shield");
            Texture2D ShieldBot = content.Load<Texture2D>("64x64/Wall_Right_Bot_Low_Shield");
            Texture2D ShieldLeft = content.Load<Texture2D>("64x64/Wall_Right_Left_Low_Shield");
            Texture2D ShieldAndSwordTop = content.Load<Texture2D>("64x64/Wall_Right_Top_Low_SwordShield");
            Texture2D ShieldAndSwordRight = content.Load<Texture2D>("64x64/Wall_Right_Right_Low_SwordShield");
            Texture2D ShieldAndSwordBot = content.Load<Texture2D>("64x64/Wall_Right_Bot_Low_SwordShield");
            Texture2D ShieldAndSwordLeft = content.Load<Texture2D>("64x64/Wall_Right_Left_Low_SwordShield");
            Texture2D wall = content.Load<Texture2D>("Wall");
            Texture2D ground = content.Load<Texture2D>("Ground");
            Texture2D DoorFront = content.Load<Texture2D>("DoorFront1");
            Texture2D Shop = content.Load<Texture2D>("Shop");
            #endregion

            #region Added Textures to list
            AddTexture(wall);
            AddTexture(piller);
            AddTexture(ground);
            AddTexture(DoorFront);
            AddTexture(wallTopCorLeft);
            AddTexture(wallTopCorLeft2);
            AddTexture(wallTopLeft);
            AddTexture(wallTopLeft2);
            AddTexture(wallMidLeftTop);
            AddTexture(wallMidLeftLow);
            AddTexture(wallBotLeft2);
            AddTexture(wallBotLeft1);
            AddTexture(wallBotCorLeft1);
            AddTexture(wallBotCorLeft2);
            AddTexture(floorPattern1);
            AddTexture(wallBotLow);
            AddTexture(wallTopLow);
            AddTexture(wallBottomLeft1);
            AddTexture(wallBottomLeft2);
            AddTexture(wallBottomMiddle1);
            AddTexture(wallBottomMiddle2);
            AddTexture(wallBottomRight2);
            AddTexture(wallBottomRight1);
            AddTexture(wallCornerBottomRight1);
            AddTexture(wallCornerBottomRight2);
            AddTexture(wallLeftBotLow);
            AddTexture(wallRightBotLow);
            AddTexture(wallRightLeft1);
            AddTexture(wallRightLeft2);
            AddTexture(wallRightMiddle1);
            AddTexture(wallRightMiddle2);
            AddTexture(wallRightTop2);
            AddTexture(wallRightTop1);
            AddTexture(wallCornerTopRight1);
            AddTexture(wallCornerTopRight2);
            AddTexture(wallLeftRightLow);
            AddTexture(wallRightRightLow);
            AddTexture(wallTopRight1);
            AddTexture(wallTopRight2);
            AddTexture(wallTopMiddle1);
            AddTexture(wallTopMiddle2);
            AddTexture(wallTopLeft_1);
            AddTexture(wallTopLeft_2);
            AddTexture(wallLeftTopLow);
            AddTexture(wallRightTopLow);
            AddTexture(floorPattern2);
            AddTexture(Door_Left_Top_Top);
            AddTexture(Door_Mid_Top_Top);
            AddTexture(Door_Right_Top_Top);
            AddTexture(Door_Left_Bot_Top);
            AddTexture(Door_Mid_Bot_Top);
            AddTexture(Door_Right_Bot_Top);
            AddTexture(Door_Left_Top_Right);
            AddTexture(Door_Mid_Top_Right);
            AddTexture(Door_Right_Top_Right);
            AddTexture(Door_Left_Bot_Right);
            AddTexture(Door_Mid_Bot_Right);
            AddTexture(Door_Right_Bot_Right);
            AddTexture(Door_Left_Top_Bottom);
            AddTexture(Door_Mid_Top_Bottom);
            AddTexture(Door_Right_Top_Bottom);
            AddTexture(Door_Left_Bot_Bottom);
            AddTexture(Door_Mid_Bot_Bottom);
            AddTexture(Door_Right_Bot_Bottom);
            AddTexture(Door_Left_Top_Left);
            AddTexture(Door_Mid_Top_Left);
            AddTexture(Door_Right_Top_Left);
            AddTexture(Door_Left_Bot_Left);
            AddTexture(Door_Mid_Bot_Left);
            AddTexture(Door_Right_Bot_Left);
            AddTexture(Brazier);
            AddTexture(Rocks);
            AddTexture(ShieldTop);
            AddTexture(ShieldRight);
            AddTexture(ShieldBot);
            AddTexture(ShieldLeft);
            AddTexture(ShieldAndSwordTop);
            AddTexture(ShieldAndSwordRight);
            AddTexture(ShieldAndSwordBot);
            AddTexture(ShieldAndSwordLeft);
            AddTexture(Carpet_Top_Left);
            AddTexture(Carpet_Top_Mid);
            AddTexture(Carpet_Top_Right);
            AddTexture(Carpet_Left_Mid);
            AddTexture(Carpet_Left_Bot);
            AddTexture(Carpet_Bot_Mid);
            AddTexture(Carpet_Bot_Right);
            AddTexture(Carpet_Right_Mid);
            AddTexture(Carpet_Mid);
            AddTexture(Door_Left_Top_Entry);
            AddTexture(Door_Mid_Top_Entry);
            AddTexture(Door_Right_Top_Entry);
            AddTexture(Door_Left_Bot_Entry);
            AddTexture(Door_Mid_Bot_Entry);
            AddTexture(Door_Right_Bot_Entry);
            AddTexture(Hole_Top_Left);
            AddTexture(Hole_Top_Mid);
            AddTexture(Hole_Top_Right);
            AddTexture(Hole_Right_Mid);
            AddTexture(Hole_Mid);
            AddTexture(Hole_Left_Mid);
            AddTexture(Hole_Bottom_Left);
            AddTexture(Hole_Bottom_Mid);
            AddTexture(Hole_Bottom_Right);
            AddTexture(Stair_Top_Left);
            AddTexture(Stair_Top_Mid);
            AddTexture(Stair_Top_Right);
            AddTexture(Stair_Bot_Left);
            AddTexture(Stair_Bot_Mid);
            AddTexture(Stair_Bot_Right);
            #endregion



        }

        /// <summary>
        /// Draws the Lake
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spritebatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = mapBackground[y, x];
                    if (textureIndex == -1)
                    {
                        continue;
                    }
                    Texture2D texture = tileTextures[textureIndex];
                    spriteBatch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
                }
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int textureIndex = map[y, x];
                    if (textureIndex == -1)
                    {
                        continue;
                    }
                    Texture2D texture = tileTextures[textureIndex];
                    spriteBatch.Draw(texture, new Rectangle(x * 64, y * 64, 64, 64), Color.White);
                }
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {

            //remove sprite if they are not needen no more
        }
        /// <summary>
        /// Update the lake map
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _gameWorld.ChangeState(new Menu(_gameWorld, _graphichsDevice, _content));
            }
        }
    }
}
