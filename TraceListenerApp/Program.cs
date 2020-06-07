//#define TRACE
//#undef TRACE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceListenerApp
{
    class Program
    {
        private static TraceSource mySource = new TraceSource("TraceListenerApp");
        static void Activity1()
        {
            mySource.TraceEvent(TraceEventType.Error, 1, "Error message.");
            mySource.TraceEvent(TraceEventType.Warning, 2, "Warning message.");
            mySource.TraceEvent(TraceEventType.Critical, 3, "Critical message.");
        }

        static void Activity2()
        {
            mySource.TraceEvent(TraceEventType.Critical, 4, "Critical message.");
            mySource.TraceInformation("Informational message.");
        }
        static void Activity3()
        {
            mySource.TraceEvent(TraceEventType.Error, 5, "Error message.");
            mySource.TraceInformation("Informational message.");
        }

        static void Main(string[] args)
        {
            BooleanSwitch dataSwitch = new BooleanSwitch("Data", "DataAccess module");
            TraceSwitch generalSwitch = new TraceSwitch("General", "Entire application");

            // if tracelistener is not set it will output in View -> Output Window

            Debug.WriteLine("DEBUG: Program started");
            Trace.WriteLine("TRACE: Program started");

            // trace to console window
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            Trace.WriteLine("TRACELISTENER: boo");

            Activity1();
            EventTypeFilter configFilter = (EventTypeFilter)mySource.Listeners["console"].Filter;

            mySource.Listeners["console"].Filter = new EventTypeFilter(SourceLevels.Critical);
            
            Activity2();

            mySource.Switch.Level = SourceLevels.All;
            mySource.Listeners["console"].Filter = configFilter;

            Activity3();

            //Trace.TraceInformation("TraceInformation test");
            //Trace.TraceWarning("TraceWarning test");
            //Trace.TraceError("TraceError test");
            int myVar = 99;
            int result = MyMethod(myVar);

            mySource.Close();

            Console.ReadKey();
        }
        private static int MyMethod(int myVar)
        {
            Trace.WriteLine("MyMethod started.");
            Trace.Assert(myVar > 100, "myVar > 100");
            myVar++;
            Trace.WriteLine("MyMethod stopped.");
            return myVar;
        }
    }
}
