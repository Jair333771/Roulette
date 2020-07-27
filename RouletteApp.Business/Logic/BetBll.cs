using RouletteApp.Core.Enums;
using RouletteApp.Core.Interfaces;
using RouletteApp.Core.Models;
using RouletteApp.Data.Emtities;
using RouletteApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApp.Business.Logic
{
    public class BetBll : AppBll
    {
        protected readonly IRepository<Roulette> rouletteRepo;
        protected readonly AppRepository<Consumer> consumerRepo;
        protected readonly BetRepository betRepo;

        protected List<int> Numbers { get; set; }
        public List<RouletteElements> MyRoulette { get; set; }

        public BetBll(ResponseModel response, MessageListModel messageList, BetRepository betRepo, AppRepository<Consumer> consumerRepo, IRepository<Roulette> rouletteRepo) : base(response, messageList)
        {
            this.betRepo = betRepo;
            this.rouletteRepo = rouletteRepo;
            this.consumerRepo = consumerRepo;
        }

        public ResponseModel RunBet(Bet bet, string userid)
        {
            object obj;
            int status;
            var result = new BetResponseModel();

            try
            {
                int.TryParse(userid, out int idUser);

                var user = consumerRepo.GetById(idUser);
                var roulette = rouletteRepo.GetById(bet.IdRoulette);

                result.Message = Validate(bet, roulette, user);

                if (result.Message.Count == 0)
                {
                    GenerateRoulette();
                    obj = RunRoulette(bet, user);
                    status = 200;
                }
                else
                {
                    status = 404;
                    obj = result.Message;
                }
            }
            catch (Exception)
            {
                status = 500;
                obj = "Ha ocurrido un error inesperado, por favor inténtalo nuevamente";
            }

            SetObjectResponse(obj, status);
            return response;
        }

        public List<RouletteElements> GenerateRoulette()
        {
            MyRoulette = new List<RouletteElements>();
            Numbers = new List<int> { 32, 19, 21, 25, 34, 27, 36, 30, 23, 5, 16, 1, 14, 9, 18, 7, 12, 3 };

            for (int i = 0; i <= 36; i++)
            {
                var element = new RouletteElements
                {
                    Value = i,
                    Color = CustomEnums.Colors.negro.ToString()
                };

                if (Numbers.Where(x => x == i).FirstOrDefault() > 0)
                {
                    element.Color = CustomEnums.Colors.rojo.ToString();
                }

                MyRoulette.Add(element);
            }

            return MyRoulette;
        }

        public List<string> Validate(Bet bet, Roulette roulette, Consumer user)
        {
            var message = new List<string>();
            var maxAmount = 10000;
            var validBetValues = Values();

            var resultBet = validBetValues.Where(x => x.Equals(bet.MyBet.ToLower())).FirstOrDefault();

            if (user == null)
            {
                message.Add("El usuario no se ha encontrado.");
            }
            else if (user.Money <= 0)
            {
                message.Add("No cuentas con saldo disponible, recarga tu saldo!.");
            }
            else if (user.Money < bet.Amount)
            {
                message.Add("El no cuenta con el suficientes recursos para realizar la apuesta, verifique su apuesta o su saldo.");
            }

            if (roulette == null)
            {
                message.Add("Ruleta no encontrada.");
            }
            else
            {
                maxAmount = roulette.MaxAmount;

                if (!roulette.State)
                {
                    message.Add("La ruleta no se encuentra habilitada para apostar.");
                }
            }

            if (bet.Amount > maxAmount)
            {
                message.Add($"La apuesta máxima permitida es de {maxAmount}.");
            }
            if (resultBet == null)
            {
                message.Add($"Debes seleccionar un color (Rojo o negro) o un número del 0 al 36.");
            }

            return message;
        }

        public static List<string> Values()
        {
            var list = new List<string>();

            for (int i = 0; i <= 38; i++)
            {
                var value = i.ToString();
                if (i == 37)
                    value = "rojo";
                else if (i == 38)
                    value = "negro";
                list.Add(value.ToString());

            }

            return list;
        }

        public BetResponseModel RunRoulette(Bet bet, Consumer user)
        {
            var response = new BetResponseModel();

            try
            {
                var rnd = new Random();
                int colorValue = 2, numberValue = 35;

                var numberResult = rnd.Next(0, 36);
                var result = MyRoulette.Where(z => z.Value == numberResult).FirstOrDefault();

                if (result != null)
                {
                    response.Number = bet.Number = result.Value;
                    response.Color = bet.Color = result.Color;

                    var text = "Has ganado!";

                    if (bet.MyBet.ToLower().Equals(response.Color))
                    {
                        user.Money += (bet.Amount * colorValue);
                    }
                    if (bet.MyBet.Equals(response.Number.ToString()))
                    {
                        user.Money += (bet.Amount * numberValue);
                    }

                    if (!bet.MyBet.Equals(response.Number.ToString()) && !bet.MyBet.ToLower().Equals(response.Color))
                    {
                        user.Money -= bet.Amount;
                        text = "Has perdido!";
                    }

                    response.Message.Add($"{text}, tu saldo actual es : {user.Money}");

                    consumerRepo.Update(user);
                    betRepo.Add(bet);
                }
                else
                {
                    response.Message.Add($"Ha ocurrido un error al realizar la ejecución, inténtalo nuevamente.");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return response;
        }
    }
}