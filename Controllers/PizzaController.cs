using Models;
using Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all. This is the code which will allow the user to see all the pizza's.
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll(); //a loop to add all the pizza's

    // GET by Id action
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id) //This code is clear on showing me how the backend will search through the application 
    {                                      //looking for the pizza with the id and how the response is handled or exception is handled
        var pizza = PizzaService.Get(id);  //I also see it as a way of handling authentication with passwords, among others.

        if(pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {            
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
            
        var existingPizza = PizzaService.Get(id);
        if(existingPizza is null)
            return NotFound();
    
        PizzaService.Update(pizza);           
    
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = PizzaService.Get(id);
    
        if (pizza is null)
            return NotFound(); 
        
        PizzaService.Delete(id);
    
        return NoContent();
    }
}