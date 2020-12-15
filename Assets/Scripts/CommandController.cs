using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Rooms;

namespace Assets.Scripts
{
    public class CommandController : MonoBehaviour
    {
        public Queue<Command> commandQueue;

        public static CommandController instance;

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);

                commandQueue = new Queue<Command>();
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddCommand(Command command, Room room, string crewName)
        {
            commandQueue.Enqueue(command);
        }
    }
}
