using PostService.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var postings = new List<Posting>
{
    new Posting
    {
        Id = 1,
        From = "Alice",
        To = "Bob",
        Content = "Books",
        DeliveryType = DeliveryType.Courier,
        Weight = 2.5f,
        Width = 30,
        Height = 20,
        Depth = 10,
        Value = 50,
        Price = 10,
        CreatedAt = DateTime.UtcNow
    },
    new Posting
    {
        Id = 2,
        From = "Anna",
        To = "Ivan",
        Content = "Clothes",
        DeliveryType = DeliveryType.Department,
        Weight = 1.5f,
        Width = 25,
        Height = 15,
        Depth = 10,
        Value = 100,
        Price = 20,
        CreatedAt = DateTime.UtcNow
    }
};

int nextId = 3;

app.MapGet("/", () => "Це API поштового клієнта.");

app.MapGet("/postings", () => postings);

app.MapGet("/postings/{id:int}", (int id) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();

    return Results.Ok(posting);
});

app.MapPost("/postings", (Posting posting) =>
{
    posting.Id = nextId++;
    posting.CreatedAt = DateTime.UtcNow;
    postings.Add(posting);
    return Results.Created($"/postings/{posting.Id}", posting);
});

app.MapPut("/postings/{id:int}", (int id, Posting updated) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();

    posting.From = updated.From;
    posting.To = updated.To;
    posting.Content = updated.Content;
    posting.DeliveryType = updated.DeliveryType;
    posting.Weight = updated.Weight;
    posting.Width = updated.Width;
    posting.Height = updated.Height;
    posting.Depth = updated.Depth;
    posting.Value = updated.Value;
    posting.Price = updated.Price;

    return Results.Ok(posting);
});

app.MapDelete("/postings/{id:int}", (int id) =>
{
    var posting = postings.FirstOrDefault(p => p.Id == id);
    if (posting is null)
        return Results.NotFound();

    postings.Remove(posting);
    return Results.NoContent();
});

app.Run();
