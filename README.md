LogCore is a simple logging manager built in C#. It uses the Singleton pattern so there is only one LogManager instance handling logs across the whole application.

The system supports three log levels: Info, Warning, and Error. When a message is logged, it is written to its corresponding file inside a logs folder, which is created automatically if it doesn’t exist. At the same time, every log entry is also saved in a central file called all.log, so all logs can be viewed in one place.

The LogManager is thread-safe, using a lock to make sure multiple threads don’t write to the files at the same time. The Singleton instance is created using Lazy<T>, keeping it simple and safe.

The project also allows reading logs either by level or from the combined log file. Overall, it demonstrates practical use of the Singleton and Factory patterns, file handling, and basic thread safety in C#.
