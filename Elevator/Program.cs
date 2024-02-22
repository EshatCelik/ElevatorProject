

using System.Drawing;
using System.Formats.Asn1;

public enum ElevatorState
{
    Idle,
    GoingUp,
    GoingDown
}
public enum PassangerState
{
    Waiting,
    InsideElevator,
    OutOfElevator
}

public class Elevator
{
    private int CurrentFlor { get; set; } = 1;
    private List<Passanger> floors;
    public ElevatorState State { get; set; }
    public int Capacity { get; set; } = 10;


    public Elevator()
    {
        State = ElevatorState.Idle;

        //MoveToFloor();

    }

    public void MoveElevatorToGoDown( int floor)
    {
        for (int i = CurrentFlor; i >= floor; i--)
        {
            Console.WriteLine($"Asansör {State.ToString()} Durumunda ,{floor}. katına iniyor... şuan {i} katında.");
            Thread.Sleep(1000);
            CurrentFlor = i;
        }
        State = ElevatorState.Idle;
        Console.WriteLine($"Asansör {State.ToString()} Durumunda ,{CurrentFlor}. katta Duruyor...");
        Thread.Sleep(1000);
        Console.WriteLine(" Kapı Açılıyor.....");
        Console.WriteLine(" Yolcuların binmesini Bekliyor.....");

        Thread.Sleep(1000);
        Console.WriteLine(" Kapı Kapanıyor");
    }
    public void MoveElevatorToGoUp(int floor)
    {
        for (int i = CurrentFlor; i <= floor; i++)
        {
            Console.WriteLine($"Asansör {State.ToString()} Durumunda {floor}. katına çıkıyor... şuan {i} katında.");
            Thread.Sleep(1000);
            CurrentFlor = i;
        }

        State = ElevatorState.Idle;
        Console.WriteLine($"Asansör {State.ToString()} Durumunda ,{CurrentFlor}. katta Duruyor...");
        Thread.Sleep(1000);
        Console.WriteLine(" Kapı Açılıyor.....");
        Console.WriteLine(" Yolcuların binmesini Bekliyor.....");

        Thread.Sleep(1000);
        Console.WriteLine(" Kapı Kapanıyor");
    }
    public void MoveToFloor()
    {
        var targetFloorList = floors.Where(x => x.State == PassangerState.Waiting).ToList();

        while (targetFloorList.Count > 0)
        {
            if (CurrentFlor > targetFloorList[0].TargetFloor)
            {
                State = ElevatorState.GoingDown;
            }
            else
            {
                State = ElevatorState.GoingUp;
            }



            if (State == ElevatorState.GoingDown)
            {
               

                MoveElevatorToGoDown(targetFloorList[0].Floor);

                if (CurrentFlor > targetFloorList[0].TargetFloor)
                {
                    State = ElevatorState.GoingDown;
                }
                else
                {
                    State = ElevatorState.GoingUp;
                }

                if (State == ElevatorState.GoingUp)
                {
                    MoveElevatorToGoUp(targetFloorList[0].TargetFloor);
                    
                    targetFloorList[0].State = PassangerState.OutOfElevator;
                    targetFloorList.RemoveAt(0);

                    Thread.Sleep(1000);
                }
                else if (State == ElevatorState.GoingDown)
                {
                    MoveElevatorToGoDown(targetFloorList[0].TargetFloor);
                   
                    targetFloorList.RemoveAt(0);
                }
                else
                {
                    State = ElevatorState.Idle;
                    Console.WriteLine($"Asansör {CurrentFlor} katında duruyor");
                }



            }
            else if (State == ElevatorState.GoingUp)
            {
                MoveElevatorToGoUp(targetFloorList[0].Floor);
                

                if (CurrentFlor > targetFloorList[0].TargetFloor)
                {
                    State = ElevatorState.GoingDown;
                }
                else
                {
                    State = ElevatorState.GoingUp;
                }


                if (State == ElevatorState.GoingUp)
                {
                    MoveElevatorToGoUp(targetFloorList[0].TargetFloor);
                    
                    targetFloorList[0].State = PassangerState.OutOfElevator;
                    targetFloorList.RemoveAt(0);

                    Thread.Sleep(1000);
                    Console.WriteLine(" Kapı Kapanıyor");
                }
                else if (State == ElevatorState.GoingDown)
                {
                    MoveElevatorToGoDown(targetFloorList[0].TargetFloor);
                    targetFloorList.RemoveAt(0);
                    Thread.Sleep(1000);
                }
                else
                {
                    State = ElevatorState.Idle;
                    Console.WriteLine($"Asansör {CurrentFlor} katında duruyor");
                }

            }

        }

        State = ElevatorState.Idle;
        Console.WriteLine($"Asansör {State.ToString()} Durumunda ,{CurrentFlor}. katta");
    }



    public void CallElevator(int floor, int targetFloor)
    {
        if (floors == null)
            floors = new List<Passanger>();
        floors.Add(new Passanger(targetFloor, floor));
    }

}



public class Passanger
{
    public int Floor { get; set; }
    public int TargetFloor { get; set; }
    public PassangerState State { get; set; }


    public Passanger(int targetFloor, int floor)
    {
        TargetFloor = targetFloor;
        Floor = floor;
        State = PassangerState.Waiting;
    }


    public void EnterElevator()
    {
        State = PassangerState.InsideElevator;
    }
    public void ExitElevator()
    {
        State = PassangerState.OutOfElevator;
    }
}


class Program
{
    static void Main(string[] args)
    {
        Elevator elevator = new Elevator();
               //current floor-- target floor
        elevator.CallElevator(1, 5);
        elevator.CallElevator(3, 1);


        elevator.MoveToFloor();







        Console.ReadLine();
    }
}