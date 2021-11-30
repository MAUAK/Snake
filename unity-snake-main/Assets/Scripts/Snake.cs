using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Criando uma lista para todos os segmentos do snake
    private List<Transform> _segments = new List<Transform>();
    //Criando a variável do prefab do segmento do snake do tipo transform
    public Transform segmentPrefab;
    //Criando a variável direção tipo vector2, e definindo ela como indo para a direita
    public Vector2 direction = Vector2.right;
    //Criando a variável  de tamanho inicial do snake (tipo int)
    public int initialSize = 4;
        
    //Chamando a função start
    private void Start()
    {
        //Chamando o método de resetar fase
        ResetState();
    }
    
    //Chamando a função update
    private void Update()
    {
        //Verificando se o eixo x da direção é diferente de zero
        if (this.direction.x != 0f)
        {
            //Verificando de o W ou a seta para cima do teclado está sendo apertado
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                //Definindo a direção como vector2.up (para cima)
                this.direction = Vector2.up;
            //Verficnado se o S ou a seta para baixo do teclado está sendo apertado    
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                //Definindo a direção como vector2.down (para baixo)
                this.direction = Vector2.down;
            }
        }
        //Verificando se o eixo y da direção é diferente de zero
        else if (this.direction.y != 0f)
        {
            //Verificando de o D ou a seta para direita do teclado está sendo apertado
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                //Definindo a direção como vector2.right (para deireita)
                this.direction = Vector2.right;
            //Verificando de o A ou a seta para esquerda do teclado está sendo apertado
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                //Definindo a direção como vector2.left (para esquerda)
                this.direction = Vector2.left;
            }
        }
    }

    //Chamando a função FixedUpdate
    private void FixedUpdate()
    {
        //Criando uma estrutura de repetição paracada segmento da lista, diminua a posição dele para menos um
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        //Criando vairáveis x e y para igualá-las a soma da posição de cada uma com a direção delas
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        //Definindo a posição como os valores de x e y definidos anteriormente
        this.transform.position = new Vector2(x, y);
    }
    
    //Chamando o método de crescer o snake Grow
    public void Grow()
    {
        //Instanciando o prefab do segmento do snake em uma variável tipo trnasform
        Transform segment = Instantiate(this.segmentPrefab);
        //Igualando a posição da variável segment para a posição do segmento da lista menos um
        segment.position = _segments[_segments.Count - 1].position;

        //Adicionando o novo segmento na lista de segmentos
        _segments.Add(segment);
    }
    
    //Criando o método de resetar fase
    public void ResetState()
    {
        //Retornando a direção para a direita
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        //Criando uma estrutura de repetição para destruir todos os game objects de segmentos
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        //Limpando a lista de segmentos
        _segments.Clear();
        _segments.Add(this.transform);

        //Criando uma estrutura de repetição para cada valor da variável de tamanho inicial, ele cresça o snake (imprimindo seus segmentos)
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Chamando a função de disparar algo assim que a colisão for ativada (por trrgger - gatilho)
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Comparando se a tar for "Food" ele creça o snake adicionando um segmento
        if (other.tag == "Food") {
            Grow();
        } 
        //Comparando se a tag for "Obstacle" chama o método de resetar a fase
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
