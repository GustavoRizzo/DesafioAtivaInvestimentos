## Desafio Ativa Investimentos

Este é um projeto correspondente ao desafio da primeira fase do processo seletivo da Ativa Investimentos. O projeto consiste em uma API Rest que permite a listagem, aplicação e resgate em fundos de investimento e uma página simples para acessar os endpoints da API no frontend.


## Build

1.  Clone o repositório
2.  Abra `AtivaAPI.sln`.
3.  Aperte F5.

## Endpoints
 **Fundos**   
`GET api/Fundos` Lista os fundos.   
`GET api/Fundos/id` Mostra um fundo específico.   
`POST api/Fundos` Adiciona um fundo novo na lista.   
`DELETE api/Fundos/id` Remove um fundo da lista.   
`PUT api/Fundos/id` Atualiza um fundo.   

**Operações (Aplicação e Resgate)**   
`GET api/Operacoes` Lista as operações.   
`GET api/Operacoes/id` Mostra uma operação específico.   
`POST api/Operacoes` Adiciona/Realiza uma nova operação.   
`DELETE api/Operacoes/id` Remove uma operação da lista.   
`PUT api/Operacoes/id` Atualiza uma operação.

## Json Format

**Fundos**
```yaml
{
    "id": "Id do Fundo" (guid)
    "nome": "Nome do Fundo" (string)
    "cnpj": "CNPJ do Fundo" (string)
    "investimentoMinimo": "Investimento Minimo" (decimal)
}
```
**Operações**
```yaml
{
    "id": "Id da operação" (guid)
    "tipoOperacao": "Tipo de Operação (Aplicação ou Resgate)" (enum) 
    "idFundo": "Id do fundo" (guid)
    "cpfCliente": "Cpf do cliente" (string)
    "valorMovimentacao": "Valor da movimentação" (decimal)
    "dataMovimentacao": "Data da movimentação" (datetime)
}
```
##### **O Id é gerado automaticamente ao utilizar o método POST.*