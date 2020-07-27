using RouletteApp.Core.Interfaces;
using RouletteApp.Core.Models;
using RouletteApp.Data.Emtities;
using RouletteApp.Data.Repositories;
using System;
using System.Collections.Generic;

namespace RouletteApp.Business.Logic
{
    public class RouletteBll : AppBll
    {
        protected readonly IRepository<Roulette> rouletteRepo;
        protected readonly IRepository<Consumer> consumerRepo;
        protected readonly BetRepository betRepo;
        protected readonly List<BetResponseModel> betResponseModelList;
        protected readonly BetResponseModel betResponseModel;

        protected List<int> Numbers { get; set; }
        public List<RouletteElements> MyRoulette { get; set; }

        public RouletteBll(ResponseModel response, MessageListModel messageList, List<BetResponseModel> betResponseModelList,
            BetResponseModel betResponseModel, IRepository<Roulette> rouletteRepo, IRepository<Consumer> consumerRepo, BetRepository betRepo) : base(response, messageList)
        {
            this.betResponseModelList = betResponseModelList;
            this.betResponseModel = betResponseModel;
            this.rouletteRepo = rouletteRepo;
            this.consumerRepo = consumerRepo;
            this.betRepo = betRepo;
        }

        public ResponseModel GetAll()
        {
            object obj;
            int status;
            try
            {
                obj = rouletteRepo.GetAll();
                status = 200;
            }
            catch (Exception ex)
            {
                status = 500;
                obj = "Ha ocurrido un error inesperado, por favor inténtalo nuevamente";
            }

            SetObjectResponse(obj, status);
            return response;
        }

        public ResponseModel GetById(int id)
        {
            object obj;
            int status;
            try
            {
                obj = rouletteRepo.GetById(id);
                status = 200;
            }
            catch (Exception ex)
            {
                status = 500;
                obj = "Ha ocurrido un error inesperado, por favor inténtalo nuevamente";
            }

            SetObjectResponse(obj, status);
            return response;
        }

        public ResponseModel Create()
        {
            object obj;
            int status;
            try
            {
                var roulette = rouletteRepo.Add(new Roulette
                {
                    MaxAmount = 10000,
                    State = false
                });

                if (roulette != null)
                {
                    status = 201;
                    obj = roulette;
                }
                else
                {
                    status = 404;
                    obj = "Ha ocurrido un error al generar la ruleta, por favor inténtelo nuevamente";
                }
            }
            catch (Exception ex)
            {
                status = 500;
                obj = "Ha ocurrido un error inesperado, por favor inténtalo nuevamente";
            }

            SetObjectResponse(obj, status);
            return response;
        }

        public ResponseModel Update(int id, bool state)
        {
            object obj;
            int status;
            try
            {
                var roulette = rouletteRepo.GetById(id);

                if (roulette != null)
                {
                    roulette.State = state;
                    obj = rouletteRepo.Update(roulette);
                    status = 202;
                }
                else
                {
                    status = 304;
                    obj = "Ha ocurrido un error al actualizar la ruleta, por favor inténtelo nuevamente";
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

        public ResponseModel Close(int id, bool state)
        {
            object obj;
            int status = 404;
            var result = new BetResponseModel();

            try
            {
                var roulette = rouletteRepo.GetById(id);
                result.Message = ValidateClose(roulette);

                if (result.Message.Count == 0)
                {
                    roulette.State = state;
                    obj = rouletteRepo.Update(roulette);

                    if (obj != null)
                    {
                        var betList = betRepo.GetByRouleteId(id);

                        foreach (var item in betList)
                        {
                            betResponseModel.Color = item.Color;
                            betResponseModel.IdRoulette = item.IdRoulette;
                            betResponseModel.Message = null;
                            betResponseModel.Number = item.Number;
                            betResponseModelList.Add(betResponseModel);
                        }

                        if (betResponseModelList.Count > 0)
                        {
                            status = 200;
                            obj = betResponseModelList;
                        }
                        else
                        {
                            obj = "La consulta no produjo ningun resultado.";
                        }
                    }
                    else
                    {
                        obj = "Ha ocurrido un error mientras se actualizaban lso datos.";
                    }
                }
                else
                {
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

        public List<string> ValidateClose(Roulette roulette)
        {
            var message = new List<string>();

            if (roulette == null)
            {
                message.Add("No hay una ruleta asignada a esta solicitud.");
            }
            else if (!roulette.State)
            {
                message.Add("La ruleta se encuentra desactivada.");
            }
            return message;
        }
    }
}