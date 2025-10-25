using _Project._Scripts.Cores.Commands; 

namespace Project._Scripts.Initilazer
{
    public class GameInitilazer : CommandExecuteHandler
    { 
        private void Start()
        {
            ExecuteCommands();
        }
    }
}