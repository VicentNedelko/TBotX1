using LogicModule.Nodes.Helpers;
using LogicModule.ObjectModel;
using LogicModule.ObjectModel.TypeSystem;
using System.Threading.Tasks;

namespace gira_com_by.Logic.Nodes
{
  public class Node : LogicNodeBase
  {
        private readonly ITypeService typeService;
        private IBot bot;

        [Parameter(DisplayOrder = 1, IsRequired = true)]
        public StringValueObject Message { get; private set; }

        [Input(DisplayOrder = 2, IsInput = true, IsRequired = true)]
        public BoolValueObject Send { get; set; }


        public Node(INodeContext context)
          : base(context)
        {
          context.ThrowIfNull("context");

            this.typeService = context.GetService<ITypeService>();
            this.Message = typeService.CreateString(PortTypes.String, "Message", "Empty");
            this.Send = typeService.CreateBool(PortTypes.Bool, "Send", false);
        }
    
        public override void Startup()
        {
        }

        public async Task Execute()
        {
            if (Send.HasValue == true)
            {
                bot = new TBot();
                await bot.SendMessageAsync(Message);
            }
        }
      }
}
