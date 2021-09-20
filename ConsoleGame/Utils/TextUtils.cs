using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGame.Utils
{

    public static class TextUtils
    {

        /// <summary>
        /// Sends a list of <c>Text</c> objects to be displayed on the screen in
        /// the <c>TextType</c> format.
        /// </summary>
        /// <param name="aMessage">A List of <c>Text</c> objects to be displayed on the screen!</param>
        /// <param name="aType">A <c>TextType</c> which decides what type of format to use!</param>
        /// <param name="aShouldWriteContinue">LETTER MESSAGES ONLY, if you should be able to skip the slow writing of a letter message</param>
        /// <exception cref="ArgumentException">Exception gets thrown when the list is either empty or only contains null values</exception>
        public static void SendMessage(IReadOnlyList<string> aMessage, TextType aType, bool aShouldWriteContinue = false)
        {
            // Makes sure the message is Non Null!
            IReadOnlyList<string> tempMessage = aMessage.Where(m => m != null).ToArray();
            
            // If the message is null throw exception
            if (tempMessage.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(aMessage));
            
            switch (aType)
            {
                case TextType.CENTERED:
                    SendCenteredMessage(tempMessage);
                    break;
                case TextType.INVENTORY:
                    SendInventoryMessage(tempMessage);
                    break;
                case TextType.MISSION:
                    SendMissionMessage(tempMessage);
                    break;
                case TextType.HUD:
                    SendHudMessage(tempMessage);
                    break;
                case TextType.EXPLANATION:
                    SendExplanationMessage(tempMessage[0], tempMessage[1]);
                    break;
                case TextType.HEADERBAR:
                    SendHeaderBarMessage(tempMessage);
                    break;
                default:
                    throw new ArgumentException("TextType is not supported!");
            }
        }

        private static void SendHudMessage(IReadOnlyList<string> aMessage)
        {
            if(aMessage.Count > 1)
                throw new ArgumentException("You cant have more than 1 message in your HUD");

            var tempX = Console.WindowWidth - aMessage[0].Length - 2;
            var tempY = Console.WindowHeight - 1;
            Console.SetCursorPosition(tempX,tempY);
            Console.Write(aMessage[0]);
        }

        /// <summary>
        /// Sends a one-line message in the <c>TextType</c> format!
        /// </summary>
        /// <param name="aMessage">A NonNull <c>Text</c> object to display in the Console</param>
        /// <param name="aType">A <c>TextType</c> which decides what type of format to use!</param>
        public static void SendMessage(string aMessage, TextType aType)
        {
            SendMessage(new[] { aMessage }, aType);
        }
        
        private static void SendCenteredMessage(IReadOnlyList<string> aMessage)
        {
            for (var i = 0; i < aMessage.Count; i++)
            {
                var tempX = Console.WindowWidth / 2 - aMessage[i].Length / 2;
                var tempY = Console.WindowHeight / 2 - aMessage.Count / 2 + i;
                Console.SetCursorPosition(tempX, tempY);
                Console.Write(aMessage[i]);
            }

        }

        private static void SendHeaderBarMessage(IReadOnlyList<string> aMessage)
        {
            for (var i = 0; i < aMessage.Count; i++)
            {
                var tempX = Console.BufferWidth / 2 - aMessage[i].Length / 2;
                Console.SetCursorPosition(tempX, i + 1);
                Console.WriteLine(aMessage[i]);
            }
        }

        private static void SendExplanationMessage(string aTitle, string aMessage)
        {
            const int tempMaxCharactersPerLine = 40;
            var tempAmountOfBreakLines = 0;

            var tempIndexOfLastBreakLine = 0;
            var tempPreviousCharacters = 0;

            var tempFinishedTextBuilder = new StringBuilder(aMessage);
            for (var i = 0; i < aMessage.Length; i++)
            {
                if (aMessage[i] == ' ')
                {
                    tempIndexOfLastBreakLine = i;
                }
                
                if (i - tempPreviousCharacters <= tempMaxCharactersPerLine) continue;
                tempFinishedTextBuilder.Replace(" ", "§", tempIndexOfLastBreakLine, 1);
                tempPreviousCharacters = i;
                tempAmountOfBreakLines++;
            }
            
            var tempTitleX = Console.WindowWidth - (tempMaxCharactersPerLine / 2 + aTitle.Length / 2) - 10;
            var tempX = Console.WindowWidth - tempMaxCharactersPerLine - 10;
            var tempY = Console.WindowHeight / 2 - tempAmountOfBreakLines / 2;

            // Needed to put the message in the right position
            var tempTextList = tempFinishedTextBuilder.ToString().Split('§');

            if(tempTitleX < tempX)
                throw new ArgumentException("Title too long");
            
            // Paste the title of the explanation message
            Console.SetCursorPosition(tempTitleX, tempY - 1);
            Console.WriteLine(ConsoleColor.Cyan + aTitle);
            
            // Paste the explanation message.
            for (var i = 0; i < tempTextList.Length; i++)
            {
                Console.SetCursorPosition(tempX, tempY + i);
                Console.WriteLine(tempTextList[i]);
            }
        }
        
        private static void SendInventoryMessage(IReadOnlyList<string> aMessage)
        {
            for (var i = 0; i < aMessage.Count; i++)
            {
                
                const int tempX = 2;
                var tempY = Math.Min(i, Console.WindowHeight - 2);

                Console.SetCursorPosition(tempX, tempY);
                if(aMessage[i] != null)
                    Console.WriteLine(aMessage[i]);
            
            }
        
        }

        /// <summary>
        /// Sends a message in the bottom left part of the screen,
        /// This place is reserved for different Missions
        /// </summary>
        /// 
        /// <param name="aMission">NOTE: This should never be longer than one NonNull Text object!</param>
        private static void SendMissionMessage(IReadOnlyList<string> aMission)
        {

            if (aMission.Count > 1)
                throw new ArgumentException("Too many lines of text!");
            const int tempX = 2;
            var tempY = Console.WindowHeight - 1;

            Console.SetCursorPosition(tempX, tempY);
            Console.Write(aMission[0]);
            
        }
    }
}