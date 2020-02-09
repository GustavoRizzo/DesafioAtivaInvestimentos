## Desafio Ativa Investimentos

Este � um projeto correspondente ao desafio da primeira fase do processo seletivo da Ativa Investimentos. O projeto consiste em uma API Rest que permite a listagem, aplica��o e resgate em fundos de investimento e uma p�gina simples para acessar os endpoints da API no frontend.


## Build

1.  Clone o reposit�rio
2.  Abra `AtivaAPI.sln`.
3.  Aperte F5.

## Endpoints
 **Fundos**   
`GET api/Fundos` Lista os fundos.   
`GET api/Fundos/id` Mostra um fundo espec�fico.   
`POST api/Fundos` Adiciona um fundo novo na lista.   
`DELETE api/Fundos/id` Remove um fundo da lista.   
`PUT api/Fundos/id` Atualiza um fundo.   

**Opera��es (Aplica��o e Resgate)**   
`GET api/Operacoes` Lista as opera��es.   
`GET api/Operacoes/id` Mostra uma opera��o espec�fico.   
`POST api/Operacoes` Adiciona/Realiza uma nova opera��o.   
`DELETE api/Operacoes/id` Remove uma opera��o da lista.   
`PUT api/Operacoes/id` Atualiza uma opera��o.

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
**Opera��es**
```yaml
{
    "id": "Id da opera��o" (guid)
    "tipoOperacao": "Tipo de Opera��o (Aplica��o ou Resgate)" (enum) 
    "idFundo": "Id do fundo" (guid)
    "cpfCliente": "Cpf do cliente" (string)
    "valorMovimentacao": "Valor da movimenta��o" (decimal)
    "dataMovimentacao": "Data da movimenta��o" (datetime)
}
```
##### **O Id � gerado automaticamente ao utilizar o m�todo POST.*