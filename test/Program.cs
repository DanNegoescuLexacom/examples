var semaphore = new SemaphoreSlim(1, 1);
var value = "hello";
var closure = () => {
    semaphore.Wait();
    Console.WriteLine(value);
    semaphore.Release();
};

semaphore.Wait();
closure();
value = "goodbye";
semaphore.Release();
Console.WriteLine(value);