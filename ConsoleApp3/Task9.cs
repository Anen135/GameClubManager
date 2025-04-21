namespace Tasks {
    class Task9 {

        static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        static int[] data = new int[100];
        public Task9() {
            for (int j = 0; j < data.Length; ++j) data[j] = j;
            Task t = RunAsync();
            System.Console.ReadLine();
            t.Wait();
            for (int i = 0; i < data.Length; i++) {
                System.Console.Write(data[i] + " ");
            }
        }

        private async static Task RunAsync() {
            await Task.WhenAll(SomeAsync(), HeavyComputationAsync());
        }

        static async Task SomeAsync(){
            Console.WriteLine("Начало работы...");
            string result = await GetDataAsync();
            Console.WriteLine(result);
            Console.WriteLine("Конец работы.");
        }

        static async Task<string> GetDataAsync() {
            Console.WriteLine("Получение данных...");
            await semaphore.WaitAsync();
            try {
                for (int i = 0; i < data.Length; i++) {
                    data[i] *= 2;
                }
            } finally {
                semaphore.Release();
            }
            await Task.Delay(2000); // Симуляция долгой операции
            return "Данные получены!";
        }
        static async Task HeavyComputationAsync() {
            Console.WriteLine("Запуск тяжёлого вычисления...");

            int result = await Task.Run(async () => {
                await semaphore.WaitAsync();
                try {
                    for (int i = 0; i < data.Length; i++) {
                        data[i] += 1;
                    }
                } finally {
                    semaphore.Release();
                }

                Thread.Sleep(3000); // Симуляция сложных расчётов
                return 42;
            });

            Console.WriteLine($"Результат вычислений: {result}");
        }
    }
}
