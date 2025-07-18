Algumas informações uteis :
------------------------------------------------------------------------------------------------------------------------
Diferença entre const e readonly:

Característica	                       const	                                         readonly
Pode mudar depois de criado            NUNCA                                             Só no construtor ou declaração
Pode usar com objetos                  Não (somente tipos primitivos)                    Sim (ex: string, List<>, etc)
Pode usar valor de método?             Não                                               Sim

Quando usar:
                    
Valor 100% fixo, conhecido em tempo de código                   Const
Valor fixo após a inicialização, mas lido dinamicamente         Readonly
--------------------------------------------------------------------------------------------------------------------------