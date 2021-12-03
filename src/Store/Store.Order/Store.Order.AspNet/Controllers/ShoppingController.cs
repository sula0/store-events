using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Domain.ErrorHandling;
using Store.Core.Infrastructure.AspNet;
using Store.Order.Application.Buyer.Commands.AddItemToCart;
using Store.Order.Application.Buyer.Commands.RemoveItemFromCart;

namespace Store.Order.AspNet.Controllers
{
    [ApiController]
    [Route("my")]
    public class ShoppingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        #region Actions
        
        [HttpPost]
        [Route("cart/actions/add-item")]
        public async Task<IActionResult> AddItemToCart(BuyerAddItemToCartCommand command)
        {
            Result addItemToCartResult = await _mediator.Send(command); // TODO: need to pick up the user id from token or something.
            return addItemToCartResult.Match(Ok, this.HandleError);
        }

        [HttpPost]
        [Route("cart/actions/remove-item")]
        public async Task<IActionResult> RemoveItemFromCart(BuyerRemoveItemFromCartCommand command)
        {
            Result removeItemFromCartResult = await _mediator.Send(command); // TODO: need to pick up the user id from token or something.
            return removeItemFromCartResult.Match(Ok, this.HandleError);
        }
        
        #endregion

        [HttpGet]
        [Route("cart")]
        public Task<IActionResult> GetCart([FromQuery] string customerId, [FromQuery] string sessionId) // TODO: need to pick up the user id and sessionId from token or something.
        {
            throw new NotImplementedException();
        }
    }
}