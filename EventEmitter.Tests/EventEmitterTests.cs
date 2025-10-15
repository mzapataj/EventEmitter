using EventEmitter.Tests.Extensions;

namespace EventEmitter.Tests
{
    public class EventEmitterTests
    {
        private IEventEmitter eventEmitter;
        private Dictionary<string, bool> isMethodCalled;
        private int callOnceResult;

        public EventEmitterTests()
        {
            eventEmitter = new EventEmitter();
            isMethodCalled = new Dictionary<string, bool>();
        }
        #region Tests
        [Fact]
        public void AddSingleMethod_EmitSingleEvent()
        {
            isMethodCalled["HelloWorld"] = false;

            eventEmitter.AddEmitEvent("HelloWorld", HelloWorld);

            Assert.True(isMethodCalled["HelloWorld"]);

        }

        [Fact]
        public void AddSeveralMethods_EmitSingleEvent()
        {
            isMethodCalled["HelloWorld"] = false;
            isMethodCalled["HelloWorld_2plus2"] = false;
            isMethodCalled["HelloWorld_5times"] = false;

            eventEmitter.AddEventListener("HelloWorld", HelloWorld);
            eventEmitter.AddEventListener("HelloWorld", HelloWorld_2plus2);
            eventEmitter.AddEventListener("HelloWorld", HelloWorld_5times);


            eventEmitter.Emit("HelloWorld", null);

            Assert.True(isMethodCalled["HelloWorld"] && isMethodCalled["HelloWorld_2plus2"] && isMethodCalled["HelloWorld_5times"]);

        }

        [Fact]
        public void AddSeveralMethods_EmitSeveralEvent()
        {
            isMethodCalled["HelloWorld"] = false;

            isMethodCalled["doStuffEvent_1"] = false;
            isMethodCalled["doStuffEvent_2"] = false;

            isMethodCalled["AddNumbersEvent_2numbers"] = false;
            isMethodCalled["AddNumbersEvent_3numbers"] = false;
            isMethodCalled["AddNumbersEvent_10numbers"] = false;

            eventEmitter.AddEventListener("HelloWorld", HelloWorld);

            eventEmitter.AddEventListener("doStuffEvent", DoStuffEvent_1);
            eventEmitter.AddEventListener("doStuffEvent", DoStuffEvent_2);

            eventEmitter.AddEventListener("AddNumbersEvent", AddNumbersEvent_2numbers);
            eventEmitter.AddEventListener("AddNumbersEvent", AddNumbersEvent_3numbers);
            eventEmitter.AddEventListener("AddNumbersEvent", AddNumbersEvent_10numbers);

            eventEmitter.Emit("HelloWorld", null);
            eventEmitter.Emit("doStuffEvent", null);
            eventEmitter.Emit("AddNumbersEvent", null);

            Assert.True(isMethodCalled["HelloWorld"] || isMethodCalled["doStuffEvent_1"] || isMethodCalled["doStuffEvent_2"] || isMethodCalled["AddNumbersEvent_2numbers"] || isMethodCalled["AddNumbersEvent_3numbers"] || isMethodCalled["AddNumbersEvent_10numbers"]);
        }


        [Fact]
        public void AddMethodsWithIntParameter_EmitEvent()
        {
            isMethodCalled["IntParameterEvent"] = false;

            eventEmitter.AddEventListener("IntParameterEvent", IntParameterEvent);

            eventEmitter.Emit("IntParameterEvent", new object[] { 1 });

            Assert.True(isMethodCalled["IntParameterEvent"]);
        }

        [Fact]
        public void AddMethodsWithReferenceTypeParameter_EmitEvent()
        {
            Foo foo = new Foo() { Name = "Foo", Description ="It's a foo" , Age = 18};        
            isMethodCalled["ReferenceTypeParameterEvent"] = false;

            eventEmitter.AddEventListener("ReferenceTypeParameterEvent", ReferenceTypeParameterEvent);

            eventEmitter.Emit("ReferenceTypeParameterEvent", new object[] { foo });

            Assert.True(isMethodCalled["ReferenceTypeParameterEvent"]);
        }

        [Fact]
        public void AddMethodOnce_EmitEvent()
        {
            callOnceResult = 0;

            isMethodCalled["OnceMethod"] = false;

            eventEmitter.Once("OnceMethod", OnceMethod);

            eventEmitter.Emit("OnceMethod", new object[] { 7 });

            eventEmitter.Emit("OnceMethod", new object[] { 777 });

            Assert.Equal(callOnceResult, 7);

        }

        #endregion Tests


        #region Events

        private void HelloWorld(object param)
        {
            Console.WriteLine("Hello World.");
            isMethodCalled["HelloWorld"] = true;
        }

        private void HelloWorld_2plus2(object param)
        {
            Console.WriteLine($"Hello World. { 2 + 2 }");
            isMethodCalled["HelloWorld_2plus2"] = true;
        }

        private void HelloWorld_5times(object param)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Hello World.");
            }
            isMethodCalled["HelloWorld_5times"] = true;
        }


        private void AddNumbersEvent_2numbers(object param)
        {
            int sum = 2 + 2;
            Console.WriteLine(sum);
            isMethodCalled["AddNumbersEvent_2numbers"] = true;

        }

        private void AddNumbersEvent_3numbers(object param)
        {
            int sum = 2 + 2 + 2;
            Console.WriteLine(sum);
            isMethodCalled["AddNumbersEvent_3numbers"] = true;
        }

        private void AddNumbersEvent_10numbers(object param)
        {
            int sum = 2 + 2 + 2 + 2 + 2 + 2 + 2 + 2 + 2 + 2;
            Console.WriteLine(sum);
            isMethodCalled["AddNumbersEvent_10numbers"] = true;
        }


        private void DoStuffEvent_1(object param)
        {            
            Console.WriteLine("doStuff 1");
            isMethodCalled["doStuffEvent_1"] = true;
        }

        private void DoStuffEvent_2(object param)
        {
            Console.WriteLine("doStuff 2");
            isMethodCalled["doStuffEvent_2"] = true;
        }



        private void IntParameterEvent(object[] param)
        {
            int num = (int) param[0];

            Console.WriteLine(num);
            isMethodCalled["IntParameterEvent"] = true;
        }


        private void ReferenceTypeParameterEvent(object[] param)
        {
            var foo = (Foo)param[0];

            Console.WriteLine("Name: {0}, Description: {1}, Age: {2}",foo.Name,foo.Description,foo.Age);
            isMethodCalled["ReferenceTypeParameterEvent"] = true;
        }

        private void OnceMethod(object[] param)
        {
            var num = (int)param[0];
            callOnceResult += num;
            isMethodCalled["OnceMethod"] = true;
        }


        #endregion Events
    }
}
