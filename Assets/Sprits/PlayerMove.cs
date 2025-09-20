using UnityEngine.InputSystem;
using UnityEngine;

void Awake()
{
    // Inicializa o Input System
    inputActions = new PlayerInputActions();
    inputActions.Player.Enable();  // Ativa o mapa "Player"

    // Associa callbacks às ações
    inputActions.Player.Jump.performed += OnJump;
}

void Start()
{
    rb = GetComponent<Rigidbody2D>();
    if (rb == null)
    {
        Debug.LogError("Rigidbody2D não encontrado no Player!");
    }
}

void Update()
{
    // Lê o input de movimento (A/D ou setas)
    moveInput = inputActions.Player.Move.ReadValue<Vector2>();
    Debug.Log("Move Input: " + moveInput);

    // Aplica movimento horizontal
    if (rb != null)
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
    }
}

void OnJump(InputAction.CallbackContext context)
{
    // Só pula se estiver no chão
    if (onGround && rb != null)
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
        onGround = false;
        Debug.Log("Pulou!");
    }
}

void OnCollisionEnter2D(Collision2D col)
{
    if (col.gameObject.CompareTag("Ground"))
    {
        onGround = true;
        Debug.Log("Colidiu com o Ground!");
    }
}

void OnDestroy()
{
    // Desativa o Input System para evitar memory leaks
    inputActions.Player.Disable();
}