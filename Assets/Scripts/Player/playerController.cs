using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//����Ϊ��꽻���Ĵ���
public interface I_M_Click
{
    void M_Click();
}
//����Ϊ���̲������ƶ�����
public interface I_K_Move
{
    void K_Movement();
}
//����Ϊ���������ƶ�����
public interface I_M_Move
{
    void M_Movement();
}
//����Ϊ���ƶ����������
public class Move:MonoBehaviour,I_K_Move,I_M_Move
{
    
    public Rigidbody2D rb;
    public float speed;
    public float jumpforce;
    public void K_Movement()
    {
        float horizontalmove;
        float facedirection;
        facedirection= Input.GetAxisRaw("Horizontal");
        horizontalmove = Input.GetAxis("Horizontal");
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
        }
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
        }
    }
    public void M_Movement()
    {
        ;
    }
}
//����Ϊ��꽻������Ĵ���

public class clickable : I_M_Move
{
    public void M_Movement()
    {
        Debug.Log("used");
    }
}

public class clickable : MonoBehaviour, I_M_Click
{
    
}
public class playerController : MonoBehaviour
{
    private clickable clickableInstance;
    public GameObject gameO;
    // Start is called before the first frame update
    public void Start()
    {
        print(gameO.name);
        clickableInstance.M_Movement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
