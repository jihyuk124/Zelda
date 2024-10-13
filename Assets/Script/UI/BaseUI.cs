using UnityEngine.EventSystems;

public class BaseUI : UIBehaviour
{
    // UI�� ���� ����
    protected bool _isOpen;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start(); 
    }

    // UI Ȱ��ȭ �޼���
    public virtual void Show()
    {
        _isOpen = true;
        gameObject.SetActive(true);
        OnShow();
    }

    // UI ��Ȱ��ȭ �޼���
    public virtual void Hide()
    {
        _isOpen = false;
        gameObject.SetActive(false);
        OnHide();
    }

    // UI �ʱ�ȭ (�ʿ��� ��� �ڽ� Ŭ�������� ������ ����)
    public virtual void Initialize()
    {
        // ���� �ʱ�ȭ �۾�
    }

    // HUD�� Window���� ���� ������ �޼���
    protected virtual void OnShow() { }
    protected virtual void OnHide() { }

    // UI�� ���¸� ���
    public void Toggle()
    {
        if (_isOpen)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
