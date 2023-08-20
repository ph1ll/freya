namespace Orleans;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Design",
    "CA1040:Avoid empty interfaces",
    Justification = "Used for extension methods to create a simple singleton grain implementation."
)]
public interface IGrainWithSingletonKey : IGrainWithGuidKey { }
