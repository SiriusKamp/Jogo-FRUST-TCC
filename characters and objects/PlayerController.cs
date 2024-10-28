using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]

public class PlayerController : MonoBehaviour
{

    public Player player;
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    public bool isWalking = false;
    public static int tempstm;

    Rigidbody2D rb2D;
    Vector2 movement = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        tempstm = player.entity.recoverystm;
        isWalking = false;
        rb2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
            if (isWalking && player.canrun)
            {
                player.entity.recoverystm = 0;
                player.entity.speed = player.entity.correndo;
                player.entity.currentstamina -= 1;
                if (player.entity.currentstamina == 0)
                    player.canrun = false;
            }
            else
            {
                player.entity.speed = 2.5f;
                player.entity.recoverystm = tempstm;
            }
        else
        if (isWalking)
        {
            player.entity.recoverystm = tempstm;
            player.entity.speed = 2.5f;
        }
        else
        {
            player.entity.recoverystm = tempstm;
            player.entity.speed = 0;
        }

        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);
        movement = new Vector2(input_x, input_y);

        if (isWalking)
        {
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);

    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * player.entity.speed * Time.fixedDeltaTime);
    }
}
