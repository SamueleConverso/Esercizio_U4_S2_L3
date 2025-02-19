using Microsoft.AspNetCore.Mvc;
using Esercizio_U4_S2_L3.Models;

namespace Esercizio_U4_S2_L3.Controllers {
    public class NuovoBigliettoController : Controller {

        private static List<Biglietto> BigliettiListaStatica = new List<Biglietto>() {
            new Biglietto {
                Id = Guid.NewGuid(),
                Nome = "Mario",
                Cognome = "Rossi",
                isRidotto = true,
                Sala = "SALA NORD"
            },
            new Biglietto {
                Id = Guid.NewGuid(),
                Nome = "Luca",
                Cognome = "Verdi",
                isRidotto = false,
                Sala = "SALA SUD"
            }
        };

        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public IActionResult Compra(Biglietto biglietto) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Add");
            }

            int bigliettiSalaNord = 0;
            int bigliettiSalaSud = 0;
            int bigliettiSalaEst = 0;

            foreach (var ticket in BigliettiListaStatica) {
                if (ticket.Sala == "SALA NORD") {
                    bigliettiSalaNord++;
                }
                if (ticket.Sala == "SALA SUD") {
                    bigliettiSalaSud++;
                }
                if (ticket.Sala == "SALA EST") {
                    bigliettiSalaEst++;
                }
            }

            bool areSaleFull = false;

            if (bigliettiSalaNord == 2 || bigliettiSalaSud == 2 || bigliettiSalaEst == 2) {
                areSaleFull = true;
            }

            if (areSaleFull) {
                return RedirectToAction("Add");
            } else {
                var nuovoBiglietto = new Biglietto() {
                    Id = Guid.NewGuid(),
                    Nome = biglietto.Nome,
                    Cognome = biglietto.Cognome,
                    Sala = biglietto.Sala,
                    isRidotto = biglietto.isRidotto,
                };

                BigliettiListaStatica.Add(biglietto);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Index() {
            var bigliettiViewModel = new BigliettoViewModel {
                Biglietti = BigliettiListaStatica
            };
            return View(bigliettiViewModel);
        }
    }
}
