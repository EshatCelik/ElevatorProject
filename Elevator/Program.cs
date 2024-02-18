

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

    public void MoveToFloor()
    {




        var list = floors.OrderByDescending(x => x.TargetFloor).ToList();

        if (State == ElevatorState.Idle)
        {
            if (list[0].Floor > list[0].TargetFloor)
                State = ElevatorState.GoingDown;
            State = ElevatorState.GoingUp;
        }




        foreach (var item in list)
        {

            if (State == ElevatorState.GoingDown)
            {


                while (CurrentFlor >= item.TargetFloor)
                {
                    Console.Write($"Asansör şuan  {CurrentFlor}.Katta ");

                    CurrentFlor--;
                    Console.WriteLine($"Asansör {State.ToString()} Durumunda ,{CurrentFlor}. katta");

                    Thread.Sleep(10000);



                }
                item.ExitElevator();
                item.State = PassangerState.OutOfElevator;
            }
            else if (State == ElevatorState.GoingUp)
            {

                while (CurrentFlor <= item.TargetFloor)
                {
                    Console.Write($"Asansör şuan  {CurrentFlor}.Katta ");

                    CurrentFlor++;
                    Console.WriteLine($"Asansör {State.ToString()} Durumunda ,{CurrentFlor}. katta");

                    Thread.Sleep(10000);
                }

                item.ExitElevator();
                item.State = PassangerState.OutOfElevator;
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
        elevator.CallElevator(2, 5);
        elevator.CallElevator(7, 5);
        elevator.CallElevator(8, 1);


        elevator.MoveToFloor();







        Console.ReadLine();
    }
}