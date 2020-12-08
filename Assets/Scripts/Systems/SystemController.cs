using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    //There should be a list here of the different systems that are created when the game begins...
    //And then each will be sent to ShipSystem to be created?
    //No wait, I will do this array of systems, first system in array equal new ReactorSystem(send in stuff)

    /*
     * 1. Set up ShipController-class
     * 2. Set up ShipSystem class
     * 3. Set up enum SystemTypes
     * 4. Set up enum SystemStates
     * 5. Set up list in SystemController of all systems.
     * 6. Set up basic properties of ShipSystem
     * 
     * 7. Set up methods in ShipSystem.
     * 
     * 8. Extend ShipSystem-class to class of ReactorSystem
     * 
     * 9. Set up Methods for ReactorSystem.
     * 
     * 10. Set up constructor for ReactorSystem.
     * 
     * 11. Add method in SystemController that checks if System is on. And if on, *then* updates system.
     * 
     * 12. Create additional subclasses, give each, their vars & their methods.
     * 
     * 13. Reflect on how currently the systems cannot go into, or out of Reboot a
     */

    public int amountOfSystems;
    public ShipSystem[] ShipSystems;


    //TODO: Turn this into initialize?
    public void Start()
    {
        //I HATE this line soooo much, but it seems liek the best way to do it, weirdly enough.
        //TODO: See if you can get it to work some other way if you use that GetValue thing later...
        amountOfSystems = SystemType.GetNames(typeof(SystemType)).Length;

        ShipSystems = new ShipSystem[amountOfSystems];

        //I do feel like there should be possible here to make this into a loop, and have index and enumtype just increase by one each time.
        //And also send them to ShipSystem, and then have ShipSystem send it on to the correct subclass based on the type of the enum...
        ShipSystems[0] = new ReactorSystem(); 

    }

    public void ShipSystemUpdate()
    {
        foreach(ShipSystem ShipSystem in ShipSystems)
        {
            if (ShipSystem.SystemState == SystemState.IsOn)
            {
                ShipSystem.Update();
            }
        }
    }
}
