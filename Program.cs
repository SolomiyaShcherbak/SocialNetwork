using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork
{
    public class Program
    {
        static void Main(string[] args)
        {
            HandleMenu handleMenu = new HandleMenu();
            handleMenu.handleInitialMenu();
        }
    }
}
