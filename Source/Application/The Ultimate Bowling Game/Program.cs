using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        private static List<Player> _players;
        private static List<Frame> _frames;
        private static int frames;

        static void Main(string[] args)
        {
            var exit = false;
            while (!exit) {
                ShowGameLogo();
                GetPlayers();
                ShowGameDetails(false);
                PlayGame();
                ShowGameDetails(true);

                Console.WriteLine();
                Console.Write("Would you like to play again? (y/n) ");
                exit = Console.ReadLine().ToLower() == "n";
            }
            

        }
        private static void PlayGame()
        {
            Console.Clear();
            Console.WriteLine($"== Game Started ==");
            Console.WriteLine(string.Empty);

            for (int i = 0; i < 10; i++)
            {
                var frameId = i + 1;
                Console.WriteLine();
                Console.WriteLine($"Entering Frame {frameId}:  ");
                foreach (var player in _players)
                {
                    int throw1, throw2 = 0;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine($"Player { player.ID }: { player.Name}");

                    Console.Write($"Enter score for Frame { frameId}, Throw 1: ");
                    var inputT1 = Console.ReadLine();

                    if (int.TryParse(inputT1, out throw1) && (throw1 > 0 && throw1 <= 9))
                    {
                        player.AddThrow(frameId, 1, inputT1);
                            Console.Write($"Enter score for Frame { frameId}, Throw 2: ");
                        var inputT2 = Console.ReadLine();

                        if (int.TryParse(inputT2, out throw2) && (throw2 > 0 && throw2 <= 9))
                        {
                            player.AddThrow(frameId, 2, inputT2);
                            continue;
                        }
                        else if (inputT2 == "/")
                        {
                            player.AddThrow(frameId, 2, inputT2);
                            continue;

                        }
                        if (inputT1 == "0-9")
                         {
                            player.AddThrow(frameId, 1, inputT1);
                            continue;
                            throw new Exception("Invalid input - need to handle error/validation better");
                         }
                    }
                    else if (inputT1.ToLower() == "x")
                    {
                        player.AddThrow(frameId, 1, inputT1);
                        continue;

                    }
                    if (inputT1 == "0-9" ) 
                    {
                        player.AddThrow(frameId, 1, inputT1);
                        
                        throw new Exception("Invalid input - need to handle error/validation better");
                    }
                    
                }
                Console.WriteLine();
                Console.WriteLine("-----");
            }
            Console.WriteLine(string.Empty);
            Console.WriteLine($"== Game Ended ==");
            Console.ReadLine();
        }


        private static void ShowGameDetails(bool showScores)
        {
            Console.Clear();
            ShowGameLogo();
            Console.WriteLine(string.Empty);
            Console.WriteLine($"=== GAME DETAILS ===");
            Console.WriteLine(string.Empty);

            Console.WriteLine($"-- Players");
            foreach (var player in _players) {
                Console.WriteLine($"Player { player.ID }: { player.Name}");
            }

            if (showScores)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine(string.Empty);

                Console.WriteLine($"-- Scores");

                var sortedList = _players.OrderBy(x => x.Score()).ThenBy(x => x.ID).ToArray();

                for (int i = 0; i < sortedList.Count(); i++)
                {
                    var player = sortedList[i];
                    Console.WriteLine($"Score Player { player.ID }: { player.Score()}");
                }

            }


            Console.WriteLine(string.Empty);
            Console.WriteLine($"=== GAME DETAILS ===");
            Console.WriteLine(string.Empty);
            Console.ReadLine();
        }


        #region Utility Methods
        private static void FrameCount()
        {
            var currentFrame = 1;
            while (currentFrame <= frames)
            {
                Console.Write($" Frame{currentFrame}:");
                var playerName = Console.ReadLine();
                _frames.Add(new Frame { Id = currentFrame });
                
                currentFrame++;
            }
            Console.ReadLine();
        }
        private static void GetPlayerNames() {

            var currentPlayer = 1;
            while (currentPlayer <= frames) {
                Console.Write($"Enter Player {currentPlayer} name:");
                var playerName = Console.ReadLine();

                _players.Add(new Player { ID = currentPlayer, Name = playerName });

                currentPlayer++;
            }
            var trailingS = frames > 1 ? "s" : string.Empty;
            Console.Write($"{frames} player{ trailingS } entered the game.");
            Console.ReadLine();
            Console.Clear();

            
        }

        private static void GetPlayers()
        {
            Console.Write("How many players? ");

            var tempPlayerCount = 0;
            var input = Console.ReadLine();
            if (!int.TryParse(input, out tempPlayerCount) || tempPlayerCount <= 0 || tempPlayerCount > 4)
            {
                Console.Write("You must enter a value between 1-4.");
                Console.ReadLine();
                Console.Clear();
                GetPlayers();
            }
            else {
                frames = tempPlayerCount;
                _players = new List<Player>();
                GetPlayerNames();
            }
        }

        private static void ShowGameLogo()
        {
            Console.Clear();
            Console.WriteLine(@"

  _______   _                                           
 |__   __| | |                                          
    | |    | |__     ___                                
    | |    | '_ \   / _ \                               
    | |    | | | | |  __/                               
    |_|    |_| |_|  \___|                               
                                                        
                                                        
  _    _   _   _     _                       _          
 | |  | | | | | |   (_)                     | |         
 | |  | | | | | |_   _   _ __ ___     __ _  | |_    ___ 
 | |  | | | | | __| | | | '_ ` _ \   / _` | | __|  / _ \
 | |__| | | | | |_  | | | | | | | | | (_| | | |_  |  __/
  \____/  |_|  \__| |_| |_| |_| |_|  \__,_|  \__|  \___|
                                                        
                                                        
  ____                       _   _                      
 |  _ \                     | | (_)                     
 | |_) |   ___   __      __ | |  _   _ __     __ _      
 |  _ <   / _ \  \ \ /\ / / | | | | | '_ \   / _` |     
 | |_) | | (_) |  \ V  V /  | | | | | | | | | (_| |     
 |____/   \___/    \_/\_/   |_| |_| |_| |_|  \__, |     
                                              __/ |     
                                             |___/      
   _____                                                
  / ____|                                               
 | |  __    __ _   _ __ ___     ___                     
 | | |_ |  / _` | | '_ ` _ \   / _ \                    
 | |__| | | (_| | | | | | | | |  __/                    
  \_____|  \__,_| |_| |_| |_|  \___|                    
                                                        
                                                        
");
        }

        private static void BowlingScore(string bowlingScore)
        {
            Console.WriteLine($"Enter Score Here: {bowlingScore}");
            Console.ReadLine();
        }

        #endregion

    }
}
