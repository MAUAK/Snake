using UnityEngine;

public class Food : MonoBehaviour
{
    //Criando uma variável para pegar o collider de um game object. Como ele está publico, terá que ser definido na unity
    public Collider2D gridArea;

    private void Start()
    {
        //Chamando ó método de randomizar as posições
        RandomizePosition();
    }

    //Criando o método de randomizar as posições
    public void RandomizePosition()
    {
        //Criando uma váriavel do tipo bounds e definindo ela como o bound do collider pego acima
        Bounds bounds = this.gridArea.bounds;

        //Criando vairáveis x e y para igualá-las a um valor aleatório entre o minimo e o máximo do bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        //Somando as posições de x e y 
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        //Tranforma o valor de x e y na nova posição da comida
        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Chamando o método de randomizar as posições (após o trigger ser disparado)
        RandomizePosition();
    }

}
