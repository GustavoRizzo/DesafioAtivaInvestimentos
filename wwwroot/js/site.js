
//Instancia as URI e arrays que irão guardar os resultados
const uriFundos = 'api/Fundos';
const uriOperacoes = 'api/Operacoes';
let fundos = [];
let operacoes = [];

//Lista todos os fundos
function getFundos() {
    fetch(uriFundos)
        .then(response => response.json())
        .then(data => displayFundos(data))
        .catch(error => console.error('Unable to get items.', error));


}

//Lista todas operações
function getOperacoes() {
    fetch(uriOperacoes)
        .then(response => response.json())
        .then(data => displayOperacoes(data))
        .catch(error => console.error('Unable to get items.', error));
}

//Funcoes  display
function displayFundos(data) {
    //Cria as constantes com os elementos da pagina e instancia o innerHTML de cada um
    const tBody = document.getElementById('fundos');
    tBody.innerHTML = '';

    //Adiciona ao elemento na pagina cada item dentro dos dados recebidos pela API
    data.forEach(item => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNodeNome = document.createTextNode(item.nome);
        td1.appendChild(textNodeNome);
        td1.classList.add("nomeDoFundo")

        let td2 = tr.insertCell(1);
        let textNodeCnpj = document.createTextNode(item.cnpj);
        td2.appendChild(textNodeCnpj);

        let td3 = tr.insertCell(2);
        let investimentoMinimoNode = document.createTextNode("R$ " + item.investimentoMinimo.toFixed(2))
        td3.appendChild(investimentoMinimoNode);

        let td4 = tr.insertCell(3);
        let textNodeFundoId = document.createTextNode(item.id);
        td4.appendChild(textNodeFundoId);

        let operacaoBotao = document.createElement("button");
        let cartIcon = document.createElement("i");
        operacaoBotao.id = "myBtn";
        operacaoBotao.setAttribute("data-nome", item.nome)
        operacaoBotao.setAttribute("data-valorMinimo", item.investimentoMinimo)
        operacaoBotao.setAttribute("data-toggle", "modal")
        operacaoBotao.setAttribute("data-target", "#modalOperacao")
        cartIcon.classList.add("fas", "fa-shopping-cart");
        operacaoBotao.appendChild(cartIcon);

        let td5 = tr.insertCell(4);
        td5.appendChild(operacaoBotao);

    });

    fundos = data;
}

function displayOperacoes(data) {
    const tBody = document.getElementById('operacoes');
    tBody.innerHTML = '';

    data.forEach(item => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNodeTipoOperacao = document.createTextNode(item.tipoOperacao);
        td1.appendChild(textNodeTipoOperacao);

        let td2 = tr.insertCell(1);
        let textNodeIdFundo = document.createTextNode(item.idFundo);
        td2.appendChild(textNodeIdFundo);

        let td3 = tr.insertCell(2);
        let textNodeCPFCliente = document.createTextNode(item.cpfCliente);
        td3.appendChild(textNodeCPFCliente);

        let td4 = tr.insertCell(3);
        let textNodeValorMovimentacao = document.createTextNode("R$ " + item.valorMovimentacao.toFixed(2));
        td4.appendChild(textNodeValorMovimentacao);

        let td5 = tr.insertCell(4);
        let textNodeDataMovimentacao = document.createTextNode(item.dataMovimentacao);
        td5.appendChild(textNodeDataMovimentacao);

    })

    operacoes = data;
}

//Funcao para realizar uma operacao (resgate ou aplicacao)
function addOperacao() {

    const addTipoTextbox = document.getElementById('add-tipo');
    const addNomeFundoTextbox = document.getElementById('add-fundo-nome');
    const addCpfTextbox = document.getElementById('add-cpf-cliente');
    const addValorTextbox = document.getElementById('add-valor-movimentacao');
    let addIdFundoTextbox = "";

    //Compara o nome do Fundo selecionado com cada Fundo na lista de fundos e pega o Id do fundo selecionado
    fundos.forEach(fundo => {
        if (addNomeFundoTextbox.value == fundo.nome) {
            addIdFundoTextbox = fundo.id
        }
    })

    //Cria o objeto que sera enviado por POST para a API
    const operacao = {
        tipoOperacao: addTipoTextbox.value.trim(),
        idFundo: addIdFundoTextbox,
        cpfCliente: addCpfTextbox.value.trim(),
        valorMovimentacao: parseFloat(addValorTextbox.value)
    };

    //Envia o objeto como POST para a endpoint 'api/Operacoes'
    fetch(uriOperacoes, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(operacao)
    })
        .then(response => response.json())
        .then(() => {
            getOperacoes();
            addTipoTextbox.value = '';
            addIdFundoTextbox.value = '';
            addCpfTextbox.value = '';
            addValorTextbox.value = '';

            //Após inserir nova operação, fechar o modal
            $('#modalOperacao').modal('toggle');
        })
        
        .catch(error => console.error('Unable to add item.', error));
}

//Chama o modal para fazer operação ao clicar no icone 
$('#modalOperacao').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget) 
    let recipient = button.data('nome') 
    let valorMinimo = button.data('valorminimo')
    let modal = $(this)

    //Passa nome do fundo como titulo e campo invisivel para comparação com id
    //e pega o valor minimo do fundo para ser usado como "min" no input number
    modal.find('.modal-title').text(recipient)
    modal.find('#add-fundo-nome').val(recipient)
    modal.find('#add-valor-movimentacao').val(valorMinimo)
    modal.find('#add-valor-movimentacao').attr({ "min": valorMinimo })
})




