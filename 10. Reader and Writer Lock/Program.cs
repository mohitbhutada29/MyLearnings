// Reader and writer lock
// Another Thread synchronization technique.

//Why have reader–writer locks when lock/Mutex exist?
//Allows multiple concurrent readers when no writer
//holds the lock; writers get exclusive access.
//Great throughput when reads dominate writes.
// In short, this type of locks allow multiple reader threads to access the shared
// resources simultaneously, but it will give exclusive access to writer threads.
// So when write operation is performed; no other thread have access
// to the critical section.
// Eg: Central Database systems.
// Shared lock. Select statement in db.
// Exclusive locks: Update statement in db.

// Exclusive locks: No other thread can access the shared resource regardless of the
// type of the threads.

//Because mutual exclusion is not always optimal.
//If your shared state supports concurrent reads, forcing all readers to
//wait behind each other (as lock or Mutex does) wastes potential parallelism.

//Use ReaderWriterLockSlim when:
//Read operations are frequent and longer than trivial.
//Write operations are infrequent but need exclusivity.
//You want to scale read throughput under contention.

//Use lock (Monitor) when:
//Simpler logic; the critical section is short (microseconds/milliseconds).
//Balanced read/write or mostly writes.
//Low contention; code clarity matters.

//Use Mutex when:
//You need to synchronize across processes (e.g., guarding a file
//or memory-mapped resource).

//**************************************************************************************************************

//Summary

// Monitor / lock

//Scope: Intra - process(threads within the same process).
//Behavior: Mutual exclusion(one thread at a time enters the critical section).
//Pros: Simple, low overhead; the lock keyword is easy to use.
//Cons: If you have many readers and only occasional writers,
//lock still serializes all access—readers block each
//other even when they could safely run concurrently.

//Mutex

//Scope: Cross - process(can synchronize across processes).
//Behavior: Mutual exclusion, kernel object, heavier than Monitor.
//Pros: Works across process boundaries.
//Cons: Higher overhead; not ideal for high-frequency
//in-memory synchronization within a single process.

//Use when you must coordinate access between
//processes (e.g., a named mutex), otherwise prefer Monitor.

//ReaderWriterLockSlim (Reader–Writer lock)

//Scope: Intra - process.
//Behavior: Allows multiple concurrent readers when no
//writer holds the lock; writers get exclusive access.
//Pros:
//Great throughput when reads dominate writes.
//Lower overhead than the older ReaderWriterLock.
//Supports upgradeable read lock (read-mostly operations that
//occasionally need to write).

//Cons:
//More complex than lock.
//Writers can be starved if readers constantly stream in
//(mitigated but still a consideration).
//Not reentrant for write lock; you must design carefully.

//Use when your data structure is read frequently
//and updated rarely, and readers can run concurrently
//without interference.

//**************************************************************************************************************

public class GlobalConfigurationCache
{
    private ReaderWriterLockSlim _lock = new();
    private Dictionary<int, string> _cache = new();

    public void Add(int key, string value)
    {
        bool lockAcquired = false;
        try
        {
            _lock.EnterWriteLock();
            lockAcquired = true;
            _cache[key] = value;   // write lock because we are updating the resource
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if(lockAcquired)
            {
                _lock.ExitWriteLock();
            }
        }
    }

    public string? Get(int key)
    {
        bool lockAcquired = false;

        try
        {
            _lock.EnterReadLock();
            lockAcquired = true;

            if (_cache.TryGetValue(key, out var value)) return value;
            else return null;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (lockAcquired)
            {
                _lock.ExitReadLock();
            }
        }
    }
}


// This is a thread safe Global Configuration Cache used within your
// application, That allows multiple reader threads and a unique writer thread
// at a given time.