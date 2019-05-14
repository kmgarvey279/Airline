[HttpGet("/cities")]
public ActionResult Index()
{
  List<City> allCities = City.GetAll();
  return View(allCities);
}

[HttpGet("/cities/new")]
public ActionResult New()
{
  return View();
}

[HttpPost("/cities")]
public ActionResult Create(string description)
{
  City newCity = new City(name);
  newCity.Save();
  List<City> allCities = City.GetAll();
  return View("Index", allCities);
}

[HttpGet("/cities/{id}")]
public ActionResult Show(int id)
{
  Dictionary<string, object> model = new Dictionary<string, object>();
  City selectedCity = City.Find(id);
  List<Flight> cityFlights = selectedCity.GetFlights();
  List<Flight> allFlights = Flight.GetAll();
  model.Add("selectedCity", selectedCity);
  model.Add("cityFlights", cityFlights);
  model.Add("allFlights", allFlights);
  return View(model);
}

[HttpPost("/cities/{cityId}/flights/new")]
public ActionResult AddFlight(int cityId, int flightId)
{
  City city = City.Find(cityId);
  Flight flight = Flight.Find(flightId);
  city.AddFlight(flight);
  return RedirectToAction("Show", new { id = cityId });
}
