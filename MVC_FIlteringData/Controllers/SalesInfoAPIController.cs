using MVC_FIlteringData.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MVC_FIlteringData.Controllers
{
    public class SalesInfoAPIController : ApiController
    {
        CompanyEntities ctx;
        public SalesInfoAPIController()
        {
            ctx = new CompanyEntities(); 
        }

        [Route("Agents")]
        public IEnumerable<Agent> GetAgents()
        {
            List<Agent> Agents = new List<Agent>();  

            var result = (from a in ctx.Agents
                          select new  { 
                           AgentId = a.AgentId,
                            AgentName = a.AgentName
                          }).ToList();


            foreach (var item in result)
            {
                Agents.Add(new Agent() {AgentId = item.AgentId,AgentName = item.AgentName });
            }

            return Agents;
        }

        [Route("Territories")]
        public IEnumerable<Territory> GetTerritories()
        {
            List<Territory> Territories = new List<Territory>();

            var result = (from a in ctx.Territories
                          select new
                          {
                              TerritoryId = a.TerritoryId,
                              TerritoryName = a.TerritoryName
                          }).ToList();


            foreach (var item in result)
            {
                Territories.Add(new Territory() {TerritoryId  = item.TerritoryId, TerritoryName = item.TerritoryName });
            }

            return Territories;
        }

        [Route("SalesInfo")]
        public IEnumerable<SalesInfo> GetSales()
        {
            List<SalesInfo> Sales = new List<SalesInfo>();

            var result = from s in ctx.Sales
                         select new { 
                            SalesRecordId = s.SalesRecordId,
                            AgentId = s.AgentId,
                            TerritoryId = s.TerritoryId,
                            ProductId = s.ProductId,
                            Quantity = s.Quantity
                         };

            foreach (var item in result)
            {
                Sales.Add(new SalesInfo()
                  {
                      SalesRecordId = item.SalesRecordId,
                      AgentName =  ctx.Agents.Where(id=>id.AgentId==item.AgentId).First().AgentName,
                      TerritoryName = ctx.Territories.Where(id=>id.TerritoryId==item.TerritoryId).First().TerritoryName,
                      ProductName = ctx.Products.Where(id=>id.ProductId==item.ProductId).First().ProductName,
                      Quantity = item.Quantity
                  });
            }

            return Sales;
        }


        /// <summary>
        /// The Method works with Multiple Parameters in the URL
        /// Here filter parameter is 'AND' or 'OR'
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="filter"></param>
        /// <param name="territoryId"></param>
        /// <returns></returns>
        [Route("Agents/{agentId}/{filter}/Territories/{territoryId}")]
        public IEnumerable<SalesInfo> GetSales(int agentId,string filter,int territoryId)
        {
            List<SalesInfo> Sales = new List<SalesInfo>();
            filter = filter.ToUpper();
             
            switch (filter)
            {
                case "AND":
                    var result1 = from s in ctx.Sales
                                 where s.AgentId == agentId && s.TerritoryId == territoryId
                                 select new
                                 {
                                     SalesRecordId = s.SalesRecordId,
                                     AgentId = s.AgentId,
                                     TerritoryId = s.TerritoryId,
                                     ProductId = s.ProductId,
                                     Quantity = s.Quantity
                                 };

                    foreach (var item in result1)
                    {
                        Sales.Add(new SalesInfo()
                        {
                            SalesRecordId = item.SalesRecordId,
                            AgentName = ctx.Agents.Where(id => id.AgentId == item.AgentId).First().AgentName,
                            TerritoryName = ctx.Territories.Where(id => id.TerritoryId == item.TerritoryId).First().TerritoryName,
                            ProductName = ctx.Products.Where(id => id.ProductId == item.ProductId).First().ProductName,
                            Quantity = item.Quantity
                        });
                    }

                    break;
                case "OR":
                   var  result2 = from s in ctx.Sales
                                 where s.AgentId == agentId || s.TerritoryId == territoryId
                                 select new
                                 {
                                     SalesRecordId = s.SalesRecordId,
                                     AgentId = s.AgentId,
                                     TerritoryId = s.TerritoryId,
                                     ProductId = s.ProductId,
                                     Quantity = s.Quantity
                                 };

                   foreach (var item in result2)
                   {
                       Sales.Add(new SalesInfo()
                       {
                           SalesRecordId = item.SalesRecordId,
                           AgentName = ctx.Agents.Where(id => id.AgentId == item.AgentId).First().AgentName,
                           TerritoryName = ctx.Territories.Where(id => id.TerritoryId == item.TerritoryId).First().TerritoryName,
                           ProductName = ctx.Products.Where(id => id.ProductId == item.ProductId).First().ProductName,
                           Quantity = item.Quantity
                       });
                   }

                    break;
            }

            return Sales;
        }



    }
}