# Unity Utilities

These classes are intended to help quickstart projects as well as provide globally usable logic for the somewhat more boilerplate code that often is required in a new project.

**Usage**
While there are classes that act as standalone Unity components, many require that client code be written to manage them. Often, these classes were written with UnityEvent usage in mind. Foreknowledge on UnityEvents might be helpful.

**Project Organization**
Typically, there will be few classes that rely on logic outside of their own namespaces. One such case where this isn't true is for UnityEventExtensions, which are not specific to any domain and find many uses.
