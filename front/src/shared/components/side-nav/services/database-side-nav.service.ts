import { Injectable } from "@angular/core";

interface TreeNode {
  name: string;
  route?: string;
  icon?: string;
  toolTip?: string;
  opened?: boolean;
  children?: TreeNode[];
}

@Injectable({ providedIn: 'root' })
export class DatabaseSideNavServices {

  dataTree: TreeNode[] = [
    {
      name: 'Clientes', icon: 'record_voice_over', toolTip: 'Clientes', opened: false,
      children: [
        { name: 'Cadastro e consulta', route: `/customers/list` },
         { name: 'Clientes', route: '/side-nav/create' },
        // { name: 'Equipamentos', route: '/side-nav/customer-dash' }
      ],
    },
    {
      name: 'Colaboradores', icon: 'group_add', toolTip: 'Bancada', opened: false,
      children: [
        { name: 'Cadastros e consulta', opened: false, route: '/employees/list' }
        // { name: 'Cadastros e consulta', opened: false, children: [{ name: 'Serviços preços', route: '/side-nav/bench-budget-service-dash/table-provided-services-prices' }] },
        //{ name: 'Serviços', opened: false, children: [{ name: 'Todos', route: `/side-nav/bench-budget-service-dash/list-services/${this.companyId}` }] },
        // {
        //   name: 'Orçamento', opened: false, children: [{ name: 'Todos', route: `/side-nav/bench-budget-service-dash/list-budgets/${this.companyId}` },
        //   { name: 'Abrir', opened: false, route: `/side-nav/bench-budget-service-dash/open-budget/${this.companyId}` }]
        // },
        // { name: 'Status', opened: false, route: '/side-nav/bench-budget-service-dash' }
      ],
    },
    {
      name: 'Equipamentos', icon: 'settings_input_composite', toolTip: 'Bancada', opened: false,
      children: [
        { name: 'Cadastros e consulta', opened: false, route: '/equipments/list' }
        // { name: 'Cadastros e consulta', opened: false, children: [{ name: 'Serviços preços', route: '/side-nav/bench-budget-service-dash/table-provided-services-prices' }] },
        //{ name: 'Serviços', opened: false, children: [{ name: 'Todos', route: `/side-nav/bench-budget-service-dash/list-services/${this.companyId}` }] },
        // {
        //   name: 'Orçamento', opened: false, children: [{ name: 'Todos', route: `/side-nav/bench-budget-service-dash/list-budgets/${this.companyId}` },
        //   { name: 'Abrir', opened: false, route: `/side-nav/bench-budget-service-dash/open-budget/${this.companyId}` }]
        // },
        // { name: 'Status', opened: false, route: '/side-nav/bench-budget-service-dash' }
      ],
    },
    //   ]
    // },
    // {
    //   name: 'Parceiros', icon: 'transfer_within_a_station', toolTip: 'Parceiros', opened: false,
    //   children: [
    //     {
    //       name: 'Cadastros e consulta', opened: false, route: `/side-nav/partner-dash/list-partner/${this.companyId}`
    //     },
    //     { name: 'Acompanhamento', opened: false, route: '/side-nav/partner-dash' }
    //   ],
    // },
    // {
    //   name: 'Terceirização', icon: 'compare_arrows', toolTip: 'Vendas', opened: false, children: [{ name: 'Reparo Eletônico', route: `/side-nav/partner-dash/create-eletronic-repair/${this.companyId}` },
    //   { name: 'Coleta Entrega', opened: false, route: `/side-nav/outsourced-dash/list-collect-deliver-all-months` },
    //   ]
    // },
    // {

    //   name: 'Estoque', icon: 'storage', toolTip: 'Estoque', opened: false,
    //   children: [
    //     {
    //       name: 'Cadastros', opened: false, children: [
    //         // { name: 'add-update-product', opened: false, route: `/side-nav/stock-product-router/add-item-product` },
    //         { name: 'Adicionar Produto', opened: false, route: `/side-nav/stock-product-router/add-product` },
    //         { name: 'Adicionar Produto-2', opened: false, route: `/side-nav/stock-product-router/add-product-n` },
    //         { name: 'Produtos', opened: false, route: `/side-nav/stock-product-router/card` },

    //       ]
    //     },
    //     { name: 'Lista Produtos', opened: false, route: `/side-nav/stock-product-router/list-product` },
    //     { name: 'Tests', opened: false, route: `/side-nav/stock-product-router/tests` },
    //     // { name: 'Lista Produtos', opened: false, route: `/side-nav/product-dash/list-product/${this.companyId}` },
    //     { name: 'Status', opened: false, route: '/side-nav/partner-dash' },

    //   ],

    // }

  ];




}
