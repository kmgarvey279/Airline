[HttpGet("/flights")]
public ActionResult Index()
{
  List<Flight> allFlights = Flight.GetAll();
  return View(allFlights);
}

[HttpGet("/flights/new")]
public ActionResult New()
{
  return View();
}

[HttpPost("/flights")]
public ActionResult Create(string flightCode);
{
  Flight newFlight = new Flight(flightCode);
  newFlight.Save();
  List<Flight> allFlights =Flight.GetAll();
  return View("Index", allFlights);
}

[HttpPost("/flights/{id}")]
public ActionResult Show(int id)
{
  Dictionary<string, object> model = new Dictionary<string, object> { };
  Flight selectedFlight = Flight.Find(id);
  List<Flight> flightCities = selectedFlight.GetCities();
  List<Flight> allCities = City.GetAll();
  model.Add("flight", selectedFlight);
  model.Add("flightCities", flightCities);
  model.Add("allCities", allCities);
  return View(model);
}
