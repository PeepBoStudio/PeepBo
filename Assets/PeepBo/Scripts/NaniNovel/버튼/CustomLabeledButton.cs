using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel.UI;
using Naninovel;
using UnityEngine.EventSystems;
using PeepBo.Managers;

/* Naninovel�� �ִ� LabeledButton�� �ٷ� ��ӹ���
 * ��ư Ŭ�� ȿ���� �߰�
 * CustomUI�� �ƴ� �⺻ UI�� �����ϱ� ���� ���� <-> CustomPeepBoButton
 */
public class CustomLabeledButton : LabeledButton
{
	RectTransform rectTransform;
	Vector3 originScale;
	bool isClicked = false;

	protected override void Start()
	{
		base.Start();

		rectTransform = GetComponent<RectTransform>();
		originScale = rectTransform.localScale;
	}

    private void Update()
    {
        if (isClicked)
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originScale * 1.05f, 0.5f);
        else
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, originScale, 0.5f);
    }

	public override void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;
		base.OnPointerDown(eventData);

		isClicked = true;
		GameManager.Audio.PlayClickSound();
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;
		base.OnPointerUp(eventData);

		isClicked = false;
	}
}
